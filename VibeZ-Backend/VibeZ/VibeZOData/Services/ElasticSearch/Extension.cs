using Polly;
using Repositories.IRepository;
using Repositories.Repository;
using VibeZOData.Models;

namespace VibeZOData.Services.ElasticSearch;

internal static class Extension
{
    public static IHostApplicationBuilder AddElasticSearch(this IHostApplicationBuilder builder)
    {
        builder.Services.AddResiliencePipeline(nameof(ElasticSearchService<SearchModel>), resiliencePipelineBuilder => resiliencePipelineBuilder
            .AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                Delay = TimeSpan.FromSeconds(2),
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Constant
            })
            .AddTimeout(TimeSpan.FromSeconds(10)));

        builder.Services.AddSingleton(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));
        builder.Services.AddScoped<DataSyncService>();
        builder.Services.AddScoped<IPlaylistRepository ,PlaylistRepository>();
        builder.Services.AddScoped<ITrackRepository , TrackRepository>(); // Thêm nếu có
        builder.Services.AddScoped<IAlbumRepository ,AlbumRepository>();
        builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

        return builder;
    }
}
