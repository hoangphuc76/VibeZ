using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper; // Thêm using cho AutoMapper
using BusinessObjects;
using VibeZDTO;

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPackageController : ControllerBase
    {
        private readonly IU_PackageRepository _userPackageRepository;
        private readonly ILogger<UPackageController> _logger;
        private readonly IMapper _mapper; // Khai báo IMapper

        public UPackageController(IU_PackageRepository userPackageRepository, ILogger<UPackageController> logger, IMapper mapper)
        {
            _userPackageRepository = userPackageRepository;
            _logger = logger;
            _mapper = mapper; // Khởi tạo IMapper
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User_packageDTO>>> GetAllUserPackages()
        {
            _logger.LogInformation("Fetching all user packages."); // Log thông tin
            var userPackages = await _userPackageRepository.GetAllUserPackages();
            var userPackageDTOs = _mapper.Map<IEnumerable<User_packageDTO>>(userPackages); // Ánh xạ sang User_packageDTO
            return Ok(userPackageDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User_packageDTO>> GetUserPackageById(Guid id)
        {
            var userPackage = await _userPackageRepository.GetUserPackageById(id);
            if (userPackage == null)
            {
                _logger.LogWarning($"No user package found "); // Log cảnh báo
                return NotFound($"No user package found ");
            }
            var userPackageDTO = _mapper.Map<User_packageDTO>(userPackage); // Ánh xạ sang User_packageDTO
            return Ok(userPackageDTO);
        }

        [HttpGet("/packageByUser/{userId}")]
        public async Task<ActionResult<User_packageDTO>> GetUserPackageByUserId(Guid userId)
        {
            _logger.LogInformation($"Fetching user package for User ID: {userId}"); // Log thông tin
            var userPackage = await _userPackageRepository.GetPackageByUserId(userId);
            if (userPackage == null)
            {
                _logger.LogWarning($"No user package found with User ID: {userId} "); // Log cảnh báo
                return NotFound($"No user package found with User ID {userId} ");
            }
            var userPackageDTO = _mapper.Map<User_packageDTO>(userPackage); // Ánh xạ sang User_packageDTO
            return Ok(userPackageDTO);
        }

        [HttpGet("/packageByPackId/{packId}")]
        public async Task<ActionResult<User_packageDTO>> GetUserPackageByPackId(Guid packId)
        {
            _logger.LogInformation($"Fetching user package for Package ID: {packId}"); // Log thông tin
            var userPackage = await _userPackageRepository.GetPackageByPackageId(packId);
            if (userPackage == null)
            {
                _logger.LogWarning($"No user package found with pack ID: {packId} "); // Log cảnh báo
                return NotFound($"No user package found with pack ID {packId} ");
            }
            var userPackageDTO = _mapper.Map<User_packageDTO>(userPackage); // Ánh xạ sang User_packageDTO
            return Ok(userPackageDTO);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUserPackage(Guid packId, Guid userId, decimal total, string paymentMethod, string typeOfPremium, DateOnly startDate, DateOnly endDate)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid when creating user package."); // Log cảnh báo
                return BadRequest(ModelState);
            }

            User_package uPackage = new User_package
            {
                PackageId = packId,
                UserId = userId,
                total = total,
                PaymentMethod = paymentMethod,
                TypeOfPremium = typeOfPremium,
                Started_Day = startDate,
                End_Day = endDate
            };

            await _userPackageRepository.AddUserPackage(uPackage);
            _logger.LogInformation($"User package created successfully with User ID: {userId} and Package ID: {packId}"); // Log thông tin
            return Ok(new { message = "User package created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateUserPackage(Guid id, [FromBody] User_packageDTO updatedUserPackageDTO) // Nhận vào User_packageDTO
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid when updating user package."); // Log cảnh báo
                return BadRequest(ModelState);
            }

            var userPackage = await _userPackageRepository.GetUserPackageById(id);
            if (userPackage == null)
            {
                _logger.LogWarning($"No user package found "); // Log cảnh báo
                return NotFound($"No user package found ");
            }

            var updatedUserPackage = _mapper.Map(updatedUserPackageDTO, userPackage); // Ánh xạ và cập nhật
            await _userPackageRepository.UpdateUserPackage(updatedUserPackage);
            _logger.LogInformation($"No user package found "); // Log thông tin
            return Ok(new { message = "User package updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUserPackage(Guid id)
        {
            var userPackage = await _userPackageRepository.GetUserPackageById(id);
            if (userPackage == null)
            {
                _logger.LogWarning($"No user package found "); // Log cảnh báo
                return NotFound($"No user package found ");
            }

            await _userPackageRepository.DeleteUserPackage(id);
            _logger.LogInformation($"No user package found "); // Log thông tin
            return Ok(new { message = "User package deleted successfully" });
        }
    }
}
