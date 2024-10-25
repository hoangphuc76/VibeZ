using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Polly;
using Polly.Registry;
using System.Security.Policy;

namespace VibeZOData.Services.Blob;

public sealed class AzuriteService(ResiliencePipelineProvider<string> pipeline, IConfiguration configuration)
    : IAzuriteService
{
    private readonly BlobContainerClient _container = new(configuration.GetConnectionString("AzureLink"),
        configuration.GetConnectionString("ContainerName"));

    private readonly ResiliencePipeline _policy = pipeline.GetPipeline(nameof(Blob));

    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var accessPolicy = new BlobAccessPolicy
        {
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
            Permissions = "r"
        };
        BlobSignedIdentifier blobSignedIdentifier = new BlobSignedIdentifier
        {
            Id = "myPolicy",
            AccessPolicy = accessPolicy
        };

        await _container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        _container.SetAccessPolicy(PublicAccessType.BlobContainer, new List<BlobSignedIdentifier> { blobSignedIdentifier });
        var blobName = Guid.NewGuid().ToString();

        var blobClient = _container.GetBlobClient(blobName);

        await _policy.ExecuteAsync(
            async token => await blobClient.UploadAsync(
                file.OpenReadStream(),
                new BlobHttpHeaders { ContentType = file.ContentType },
                cancellationToken: token),
            cancellationToken);

        return blobClient.Uri.ToString();
    }
    public async Task<string> UpdateFileAsync(IFormFile file, string uri, CancellationToken cancellationToken = default)
    {
        Uri url = new Uri(uri);
        string fileName = Path.GetFileName(url.LocalPath);
        var blobClient = _container.GetBlobClient(fileName);

        await _policy.ExecuteAsync(
           async token => await blobClient.UploadAsync(
               file.OpenReadStream(),
               new BlobHttpHeaders { ContentType = file.ContentType },
               cancellationToken: token),
           cancellationToken);
        return blobClient.Uri.ToString();
    }

    public async Task DeleteFileAsync(string url, CancellationToken cancellationToken = default)
    {
        Uri uri = new Uri(url);
        string fileName = Path.GetFileName(uri.LocalPath);
        var blobClient = _container.GetBlobClient(fileName);

        await _policy.ExecuteAsync(
            async token =>
                await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: token),
            cancellationToken);
    }
}
