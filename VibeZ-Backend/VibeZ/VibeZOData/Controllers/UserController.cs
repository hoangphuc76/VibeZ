﻿using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using Repositories.UnitOfWork;
using Repositories.UnitOfWork.Repositories.UnitOfWork;
using Service.IServices;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

namespace VibeZOData.Controllers
{
    [Route("odata/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        // Constructor
        public UserController(IUserService userService, IUnitOfWork unitOfWork, ILogger<UserController> logger)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: odata/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            _logger.LogInformation("Fetching all users...");
            var users = await _unitOfWork.Users.GetAll();

            if (users == null || !users.Any())
            {
                _logger.LogWarning("No users found.");
                return NotFound("No users found.");
            }

            _logger.LogInformation("Successfully retrieved all users.");
            return Ok(users);
        }

        // GET odata/User/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUserByID(Guid id)
        {
            _logger.LogInformation($"Fetching user with ID: {id}");
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }

            _logger.LogInformation($"Successfully retrieved user with ID: {id}");
            return Ok(user);
        }

        // POST odata/User
        [HttpPost]
        public async Task<ActionResult> Post(string? name, string email, string password, string gender, string username, int yy, int mm, int dd)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Trả về lỗi nếu dữ liệu không hợp lệ
            }
            if (yy < 0 || yy > 2024 || mm < 0 || mm > 12 || dd < 0 || dd > 31)
            {
                return BadRequest("Invalid time values.");
            }
            var dob = new DateOnly(yy, mm, dd);

            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Gender = gender,
                UserName = username,
                DOB = dob
            };
            _logger.LogInformation("Adding new user...");
            await _userService.AddUser(user);

            _logger.LogInformation("User added successfully.");
            return CreatedAtAction(nameof(GetUserByID), new { id = user.Id }, user);
        }

        // PUT odata/User/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, string? name, string email, string password, string gender, string username, int yy, int mm, int dd)
        {

            var existingUser = await _unitOfWork.Users.GetById(id);
            if (existingUser == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }

            _logger.LogInformation($"Updating user with ID: {id}");
            if (yy < 0 || yy > 2024 || mm < 0 || mm > 12 || dd < 0 || dd > 31)
            {
                return BadRequest("Invalid time values.");
            }
            var dob = new DateOnly(yy, mm, dd);

            existingUser.Name = name;
            existingUser.Email = email;
            existingUser.Password = password;
            existingUser.Gender = gender;
            existingUser.UserName = username;
            existingUser.DOB = dob;
            existingUser.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.Complete();

            _logger.LogInformation($"User with ID {id} updated successfully.");
            return Ok("Updated successfully.");
        }
        // PUT odata/User/{id}
        [HttpPut("/premium/{id:guid}")]
        public async Task<ActionResult> UdPremium(Guid id, string premium)
        {

            var existingUser = await _unitOfWork.Users.GetById(id);


            existingUser.Premium = premium;
           
            existingUser.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.Complete();

            _logger.LogInformation($"User with ID {id} updated successfully.");
            return Ok("Updated successfully.");
        }
        [HttpPut("role/{id:guid}")]
        public async Task<ActionResult> UdRoleArtist(Guid id)
        {

            var existingUser = await _unitOfWork.Users.GetById(id);


            existingUser.Role = "Artist";

            existingUser.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.Complete();

            _logger.LogInformation($"User with ID {id} updated successfully.");
            return Ok("Updated successfully.");
        }
        // DELETE odata/User/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");

            var existingUser = await _unitOfWork.Users.GetById(id);
            if (existingUser == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }

            await _unitOfWork.Users.Delete(existingUser);
            await _unitOfWork.Complete();
            _logger.LogInformation($"User with ID {id} deleted successfully.");

            return Ok("Deleted successfully.");
        }

        // GET odata/User/Admin - Admin-only endpoint
        [HttpGet("Admin")]
        [Authorize(Roles = "admin")]
        public ActionResult GetAdminInfo()
        {
            var userName = User.Identity?.Name;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            _logger.LogInformation($"Admin accessed: {userName}, Role: {userRole}");

            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Unable to retrieve admin info.");
                return Unauthorized("Unable to retrieve admin information.");
            }

            return Ok(new { UserName = userName, Role = userRole });
        }
    }
}