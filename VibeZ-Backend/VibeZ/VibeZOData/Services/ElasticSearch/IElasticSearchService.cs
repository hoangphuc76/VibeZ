using Nest;
using VibeZOData.Models;

namespace VibeZOData.Services.ElasticSearch
{
    public interface IElasticSearchService<T> where T : class
    {
        Task<string> IndexDocumentAsync(string id ,T document, CancellationToken cancellationToken = default);
        Task<SearchModel> SearchDocumentAsync(string query, string? fieldName2, CancellationToken cancellationToken = default);
        Task DeleteDocumentAsync(string documentId, CancellationToken cancellationToken = default);
    }
}
