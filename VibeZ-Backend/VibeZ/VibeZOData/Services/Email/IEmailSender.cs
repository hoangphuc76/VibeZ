namespace VibeZOData.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string otpCode);
        Task SendNoticeEmail(string toEmail, string subject, string content);
    }

}