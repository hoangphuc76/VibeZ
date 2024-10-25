using Azure;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.IRepository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VibeZOData.Models.Login;

namespace VibeZOData.Controllers
{
    [Route("odata")]
    [ApiController]
    public class AuthenticateController(IUserRepository _userRepository, IConfiguration _config) : ControllerBase
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

                // Generate JWT token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:ValidIssuer"],
                    audience: _config["Jwt:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Return the token in the response
                return Ok(new { Token = tokenString, Username = user.UserName, Id = user.Id});
            }

            return NotFound("User not found");
        }
        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] UserRegister model)
        //{
        //    var userExists = await _userRepository.FindByNameAsync(model.Username!);
        //    if (userExists != null)
        //        return Conflict(new { message = "Username already exists" });

        //    User user = new User
        //    {
        //        UserName = model.Username!,
        //        Email = model.Email!,
        //        Password = model.Password!
        //    };
        //    await _userRepository.AddUser(user);

        //    return Ok("Inserted Successfully");
        //}
    }
}