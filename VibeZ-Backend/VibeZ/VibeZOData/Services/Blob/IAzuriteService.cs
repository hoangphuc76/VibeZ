namespace VibeZOData.Services.Blob;

public interface IAzuriteService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task<string> UpdateFileAsync(IFormFile file, string uri, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string url, CancellationToken cancellationToken = default);
}
