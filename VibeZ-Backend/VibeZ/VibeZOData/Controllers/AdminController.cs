using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Repositories.IRepository;
using VibeZDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminRepository adminRepository, ILogger<AdminController> _logger) : ControllerBase
    {
        [HttpGet("artist-data")]
        public async Task<ActionResult<IEnumerable<AdminArtistDTO>>> GetAllAdminArtists()
        {
            try
            {
                var artistData = await adminRepository.GetAdminArtists();
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
            var totalData = await adminRepository.GetTotalData();
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
                var adminHome = await adminRepository.GetAdminHome();
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
                var adminBan = await adminRepository.GetAdminBan();
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
                var adminApproval = await adminRepository.GetAdminApporval();
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
                await adminRepository.ChangeStatusApporval(trackId);
                return Ok("Track status changed to approved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error changing track status: {ex.Message}");
            }
        }
    }
}