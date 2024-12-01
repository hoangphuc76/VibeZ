namespace VibeZOData.Services.Email
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string subject, string trackTitle);
    }
}