using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Repositories.IRepository;
using Service.IServices;
using VibeZDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminService _adminService, ILogger<AdminController> _logger) : ControllerBase
    {
        [HttpGet("artist-data")]
        public async Task<ActionResult<IEnumerable<AdminArtistDTO>>> GetAllAdminArtists()
        {
            try
            {
                var artistData = await _adminService.GetAdminArtists();
                if (artistData == null || !artistData.Any())
                {
                    return NotFound("No artist data available.");
                }
                return Ok(artistData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching artist data.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("total-data")]
        public async Task<ActionResult<int>> GetTotalData()
        {
            var totalData = await _adminService.GetTotalData();
            if (totalData is null)
            {
                return NotFound();
            }
            return Ok(totalData);
        }
        [HttpGet("admin-home")]
        public async Task<ActionResult<AdminHomeDTO>> GetAdminHome()
        {
            try
            {
                var adminHome = await _adminService.GetAdminHome();
                return adminHome;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching admin home data.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpGet("admin-ban")]
        public async Task<ActionResult> GetAdminBan()
        {
            try
            {
                var adminBan = await _adminService.GetAdminBan();
                return Ok(adminBan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching admin ban data.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpGet("admin-approval")]
        public async Task<ActionResult> GetAdminApproval()
        {
            try
            {
                var adminApproval = await _adminService.GetAdminApproval();
                return Ok(adminApproval);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching admin approval data.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpPut("approve-track/{trackId}")]
        public async Task<ActionResult> ChangeStatusApproval(Guid trackId)
        {
            try
            {
                await _adminService.ChangeStatusApproval(trackId);
                return Ok("Track status changed to approved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error changing track status: {ex.Message}");
            }
        }
    }
}