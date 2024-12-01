using BusinessObjects;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VibeZOData.Models;
using VibeZOData.Models.Login;
using VibeZOData.Services.Email;

namespace VibeZOData.Controllers
{
    [Route("odata")]
    [ApiController]
    public class AuthenticateController(IUserRepository _userRepository, IConfiguration _config, IEmailSender _emailSender, IPasswordResetService _passwordResetService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userRepository.Authenticate(userLogin.Username, userLogin.Password);

            if (user != null)
            {

                // Create the security key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create claims based on user details
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var token = GenerateJwtToken(claims);

                return Ok(new { Token = token, User = user });
            }

            return NotFound("User not found");
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var payload = await VerifyGoogleTokenAsync(request.Token);
            Console.WriteLine(request.Token);
            if (payload == null)
            {
                return BadRequest("Invalid Google token");
            }

            var user = await _userRepository.FindByEmailAsync(payload.Email);
            if (user == null)
            {

                user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = payload.Name!,
                    UserName = payload.Email!,
                    Email = payload.Email!,
                    Password = "GoogleLogin"
                };
                Console.WriteLine($"User Info: Id = {user.Id}, Email = {user.Email}, Name = {user.Name}, UserName = {user.UserName}, Password = {user.Password}");
                await _userRepository.AddUser(user);
            }

            var claims = new[]
              {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

            var token = GenerateJwtToken(claims);

            return Ok(new { Token = token, User = user });
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings();
            return await GoogleJsonWebSignature.ValidateAsync(token, settings);
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister model)
        {
            var userExists = await _userRepository.FindByNameAsync(model.Username!);
            if (userExists != null)
                return Conflict(new { message = "Username already exists" });

            var emailExists = await _userRepository.FindByEmailAsync(model.Email!);
            if (emailExists != null)
                return Conflict(new { message = "Email already exists" });

            User user = new User
            {
                Id = Guid.NewGuid(),
                Name = model.Name!,
                UserName = model.Username!,
                Email = model.Email!,
                Password = model.Password!
            };
            await _userRepository.AddUser(user);

            return Ok("Inserted Successfully");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailRequest request)
        {
            try
            {
                await _passwordResetService.ResetPasswordAsync(request.Email);
                return Ok("Password reset email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            var isValid = _passwordResetService.ValidateOtp(request.Email, request.Otp);
            if (isValid)
            {
                return Ok(new { message = "OTP is valid" });
            }
            return BadRequest(new { message = "Invalid or expired OTP" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
        {
            var isValid = _passwordResetService.ValidateOtp(request.Email, request.Otp);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid or expired OTP" });
            }

            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            await _userRepository.UpdatePassword(user.Id, request.NewPassword);

            _passwordResetService.InvalidateOtp(request.Email);

            return Ok(new { message = "Password reset successfully" });
        }
    }
}