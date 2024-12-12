using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using Repositories.Repository;
using Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FollowController> _logger;

        public FollowController(IUnitOfWork unitOfWork, ILogger<FollowController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: odata/Follow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetAll()
        {
            _logger.LogInformation("GetAll called to retrieve all follow relationships.");
            var list = await _unitOfWork.Follows.GetAll();
            _logger.LogInformation("Retrieved {Count} follow relationships.", list.Count());
            return Ok(list);
        }

        // GET odata/Follow/User/{userId}/Artist/{artistId}
        [HttpGet("{userId}/{artistId}")]
        public async Task<ActionResult<Follow>> GetFollowByIDs(Guid userId, Guid artistId)
        {
            _logger.LogInformation("GetFollowByIDs called with UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            var follow = await _unitOfWork.Follows.GetFollowById(userId, artistId);
            if (follow == null)
            {
                _logger.LogWarning("Follow relationship not found for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
                return NotFound("Follow relationship not found");
            }
            _logger.LogInformation("Follow relationship found for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            return Ok(follow);
        }

        // POST odata/Follow
        [HttpPost("/Follow")]
        public async Task<ActionResult> Follow([FromForm]Guid userId, [FromForm]Guid artistId)
        {
            _logger.LogInformation("Follow called to create follow relationship for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            var fl = new Follow
            {
                UserId = userId,
                ArtistId = artistId,
                CreateDate = DateOnly.FromDateTime(DateTime.Now)
            };
            await _unitOfWork.Follows.Add(fl);
            await _unitOfWork.Complete();

            _logger.LogInformation("Follow relationship created for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            return CreatedAtAction(nameof(GetFollowByIDs), new { fl.UserId, fl.ArtistId }, fl);
        }

        // PUT odata/Follow/User/{userId}/Artist/{artistId}
        [HttpPut("/Unfollow/{userId}/{artistId}")]
        public async Task<ActionResult> Put(Guid userId, Guid artistId)
        {
            _logger.LogInformation("Unfollow called to update follow status for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            var existingFollow = await _unitOfWork.Follows.GetFollowById(userId, artistId);
            if (existingFollow == null)
            {
                _logger.LogWarning("Follow relationship not found for Unfollow with UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
                return NotFound("Follow relationship not found");
            }
            existingFollow.IsFollow = false;
            await _unitOfWork.Follows.Update(existingFollow);
            await _unitOfWork.Complete();

            _logger.LogInformation("Follow relationship updated to unfollow for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            return NoContent();
        }

        // DELETE odata/Follow/User/{userId}/Artist/{artistId}
        [HttpDelete("{userId}/{artistId}")]
        public async Task<ActionResult> Delete(Guid userId, Guid artistId)
        {
            _logger.LogInformation("Delete called to remove follow relationship for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            var follow = await _unitOfWork.Follows.GetFollowById(userId, artistId);
            if (follow == null)
            {
                _logger.LogWarning("Follow relationship not found for Delete with UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
                return NotFound("Follow relationship not found");
            }
            await _unitOfWork.Follows.Delete(follow);
            await _unitOfWork.Complete();
            _logger.LogInformation("Follow relationship deleted for UserId: {UserId} and ArtistId: {ArtistId}.", userId, artistId);
            return NoContent();
        }
    }
}
