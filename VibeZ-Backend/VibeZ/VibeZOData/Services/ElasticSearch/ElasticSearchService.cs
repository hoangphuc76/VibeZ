using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Polly.Registry;
using Polly;
using VibeZDTO;
using VibeZOData.Models;
using VibeZOData.Services.ElasticSearch;

public sealed class ElasticSearchService<T> : IElasticSearchService<T> where T : class, INameable
{
    private readonly IElasticClient _client;
    private readonly ResiliencePipeline _policy;

    public ElasticSearchService(ResiliencePipelineProvider<string> pipeline, IConfiguration configuration)
    {
        // Kiểm tra null cho pipeline và configuration trước khi sử dụng
        _policy = pipeline?.GetPipeline(nameof(ElasticSearchService<T>))
                  ?? throw new ArgumentNullException(nameof(pipeline), "Pipeline cannot be null.");

        var url = configuration["Elasticsearch:Url"];
        if (string.IsNullOrEmpty(url))
            throw new ArgumentNullException(nameof(url), "Elasticsearch URL cannot be null or empty.");

        var settings = new ConnectionSettings(new Uri(url))
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
            .BasicAuthentication("elastic", "7pTp=mA8vf_Jd9TAzQM-") // Thay bằng mật khẩu của bạn
            .DefaultIndex(typeof(T).Name.ToLower());

        _client = new ElasticClient(settings) ?? throw new InvalidOperationException("Failed to initialize ElasticClient.");
    }

    private async Task EnsureIndexExists(string indexName)
    {
        if (string.IsNullOrEmpty(indexName))
            throw new ArgumentNullException(nameof(indexName), "Index name cannot be null or empty.");

        var existsResponse = await _client.Indices.ExistsAsync(indexName);
        if (existsResponse == null || !existsResponse.Exists)
        {
            var createIndexResponse = await _client.Indices.CreateAsync(indexName, c => c
                .Settings(s => s
                    .Setting("index.mapping.total_fields.limit", 10000)
                    .Analysis(a => a
                        .CharFilters(cf => cf
                            .PatternReplace("remove_accents_a", pr => pr.Pattern("[áàảãạâấầẩẫậăắằẳẵặ]").Replacement("a"))
                            .PatternReplace("remove_accents_e", pr => pr.Pattern("[éèẻẽẹêếềểễệ]").Replacement("e"))
                            .PatternReplace("remove_accents_i", pr => pr.Pattern("[íìỉĩị]").Replacement("i"))
                            .PatternReplace("remove_accents_o", pr => pr.Pattern("[óòỏõọôốồổỗộơớờởỡợ]").Replacement("o"))
                            .PatternReplace("remove_accents_u", pr => pr.Pattern("[úùủũụưứừửữự]").Replacement("u"))
                            .PatternReplace("remove_accents_y", pr => pr.Pattern("[ýỳỷỹỵ]").Replacement("y"))
                            .PatternReplace("remove_accents_d", pr => pr.Pattern("[đ]").Replacement("d"))
                        )
                        .Analyzers(an => an
                            .Custom("vietnamese_custom_analyzer", ca => ca
                                .Tokenizer("standard")
                                .CharFilters("remove_accents_a", "remove_accents_e", "remove_accents_i",
                                             "remove_accents_o", "remove_accents_u", "remove_accents_y",
                                             "remove_accents_d")
                                .Filters("lowercase")
                            )
                        )
                    )
                )
                .Map<T>(m => m.AutoMap().Properties(ps => ps
                    .Text(t => t
                        .Name(n => n.Name)
                        .Analyzer("vietnamese_custom_analyzer")
                    )
                ))
            );

            if (!createIndexResponse.IsValid)
            {
                throw new Exception($"Failed to create index '{indexName}': {createIndexResponse.ServerError?.Error.Reason}");
            }
        }
    }

    public async Task<string> IndexDocumentAsync(string id, T document, CancellationToken cancellationToken = default)
    {
        await EnsureIndexExists(_client.ConnectionSettings.DefaultIndex);

        try
        {
            var json = JsonConvert.SerializeObject(document);
            Console.WriteLine($"Serialized JSON for document with ID {id}: {json}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Serialization error for document with ID {id}: {ex.Message}");
        }

        var response = await _policy.ExecuteAsync(
            async token => await _client.IndexAsync(document, i => i
                .Id(id)
                .Refresh(Refresh.WaitFor),
                token),
            cancellationToken);

        if (!response.IsValid)
        {
            throw new Exception(response.DebugInformation);
        }

        return response.Id;
    }

    public async Task<SearchModel> SearchDocumentAsync(string query, string? fieldname, CancellationToken cancellationToken = default)
    {
        await EnsureIndexExists(_client.ConnectionSettings.DefaultIndex);

        var multiSearchDescriptor = new MultiSearchDescriptor()
            .Search<PlaylistDTO>("playlistdto", s => s
                .Index("playlistdto")
                .Query(q => q.Match(m => m.Field(f => f.Name).Query(query).Fuzziness(Fuzziness.Auto)))
                .Sort(sort => sort.Descending(SortSpecialField.Score))
            )
            .Search<TrackDTO>("trackdto", s =>
            {
                var trackQuery = s.Index("trackdto")
                    .Query(q =>
                    {
                        var queryContainer = q.Match(m => m.Field(f => f.Name).Query(query).Fuzziness(Fuzziness.Auto));
                        if (!string.IsNullOrEmpty(fieldname))
                        {
                            queryContainer = queryContainer || q.Match(m => m.Field(f => f.Lyrics).Query(query).Fuzziness(Fuzziness.Auto));
                        }
                        return queryContainer;
                    })
                    .Sort(sort => sort.Descending(SortSpecialField.Score));
                return trackQuery;
            })
            .Search<AlbumDTO>("albumdto", s => s
                .Index("albumdto")
                .Query(q => q.Match(m => m.Field(f => f.Name).Query(query).Fuzziness(Fuzziness.Auto)))
                .Sort(sort => sort.Descending(SortSpecialField.Score))
            )
            .Search<ArtistDTO>("artistdto", s => s
                .Index("artistdto")
                .Query(q => q.Match(m => m.Field(f => f.Name).Query(query).Fuzziness(Fuzziness.Auto)))
                .Sort(sort => sort.Descending(SortSpecialField.Score))
            );

        var multiResponse = await _client.MultiSearchAsync(multiSearchDescriptor, cancellationToken);

        if (!multiResponse.IsValid)
        {
            throw new Exception(multiResponse.DebugInformation);
        }

        var result = new SearchModel
        {
            Playlists = multiResponse.GetResponse<PlaylistDTO>("playlistdto")?.Documents?.ToList() ?? new List<PlaylistDTO>(),
            Tracks = multiResponse.GetResponse<TrackDTO>("trackdto")?.Documents?.ToList() ?? new List<TrackDTO>(),
            Albums = multiResponse.GetResponse<AlbumDTO>("albumdto")?.Documents?.ToList() ?? new List<AlbumDTO>(),
            Artists = multiResponse.GetResponse<ArtistDTO>("artistdto")?.Documents?.ToList() ?? new List<ArtistDTO>(),
        };
        var exactMatchTopHit = multiResponse.AllResponses
        .OfType<ISearchResponse<object>>() // Đảm bảo chỉ lấy các phản hồi tìm kiếm
        .SelectMany(response => response.Hits)
        .Where(hit => ((dynamic)hit.Source).Name?.ToString().Equals(query, StringComparison.OrdinalIgnoreCase) == true)
        .OrderByDescending(hit => hit.Score)
        .FirstOrDefault();

        // Nếu có tài liệu khớp chính xác với `query`, gán vào `TopResult`; nếu không, lấy tài liệu có `score` cao nhất
        var topHit = exactMatchTopHit
            ?? multiResponse.AllResponses
                .OfType<ISearchResponse<object>>()
                .SelectMany(response => response.Hits)
                .OrderByDescending(hit => hit.Score)
                .FirstOrDefault();
        result.TopResult = topHit?.Source;

        return result;
    }

    public async Task DeleteDocumentAsync(string documentId, CancellationToken cancellationToken = default)
    {
        await EnsureIndexExists(_client.ConnectionSettings.DefaultIndex);

        var response = await _policy.ExecuteAsync(
            async token => await _client.DeleteAsync(DocumentPath<T>.Id(documentId), ct: token),
            cancellationToken);

        if (!response.IsValid)
        {
            throw new Exception(response.DebugInformation);
        }
    }
}
