namespace VibeZOData.Services.Email
{
    public interface IPasswordResetService
    {
        Task ResetPasswordAsync(string userEmail);
        bool ValidateOtp(string userEmail, string otpCode);
        void InvalidateOtp(string userEmail);
        string GenerateOtpCode();
        //void StoreOtp(string userEmail, string otpCode);
    }
}