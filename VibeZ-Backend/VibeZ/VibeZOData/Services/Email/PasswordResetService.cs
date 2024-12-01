using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Repositories.IRepository;

namespace VibeZOData.Services.Email
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache;
        public PasswordResetService(IEmailSender emailSender, IUserRepository userRepository, IMemoryCache cache)
        {
            _emailSender = emailSender;
            _userRepository = userRepository;
            _cache = cache;
        }
        public async Task ResetPasswordAsync(string userEmail)
        {
            var user = await _userRepository.FindByEmailAsync(userEmail);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var otpCode = GenerateOtpCode();
            StoreOtp(userEmail, otpCode);
            await _emailSender.SendEmailAsync(userEmail, "Password Reset", otpCode);
        }

        public string GenerateOtpCode()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        public void InvalidateOtp(string userEmail)
        {
            _cache.Remove(userEmail); // Remove the OTP once used or expired
        }
        public void StoreOtp(string userEmail, string otpCode)
        {
            var otpData = (Otp: otpCode, Expiry: DateTime.UtcNow.AddMinutes(10));
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
            _cache.Set(userEmail, otpData, cacheEntryOptions);
        }

        public bool ValidateOtp(string userEmail, string otpCode)
        {
            if (_cache.TryGetValue(userEmail, out (string Otp, DateTime Expiry) otpData))
            {
                return otpData.Otp == otpCode && DateTime.UtcNow <= otpData.Expiry;
            }
            Console.WriteLine($"No OTP found for email: {userEmail}");
            return false;
        }
    }
}