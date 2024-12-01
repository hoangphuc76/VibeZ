namespace VibeZOData.Models.Login
{
    public class PasswordResetRequest
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
}