using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("odata/Follow")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _followRepository;
            
        public FollowController()
        {
            _followRepository = new FollowRepository();
        }

        // GET: odata/Follow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetAll()
        {
            var list = await _followRepository.GetAllFollows();
            return Ok(list);
        }

        // GET odata/Follow/User/{userId}/Artist/{artistId}
        [HttpGet("{userId}/{artistId}")]
        public async Task<ActionResult<Follow>> GetFollowByIDs(Guid userId, Guid artistId)
        {
            var follow = await _followRepository.GetFollowById(userId, artistId);
            if (follow == null)
            {
                return NotFound("Follow relationship not found");
            }
            return Ok(follow);
        }

        // POST odata/Follow
        [HttpPost]
        public async Task<ActionResult> Post(Follow follow)
        {
            await _followRepository.AddFollows(follow);
            return CreatedAtAction(nameof(GetFollowByIDs), new { follow.UserId, follow.ArtistId }, follow);
        }

        // PUT odata/Follow/User/{userId}/Artist/{artistId}
        [HttpPut("{userId}/{artistId}")]
        public async Task<ActionResult> Put(Guid userId, Guid artistId, Follow follow)
        {
            var existingFollow = await _followRepository.GetFollowById(userId, artistId);
            if (existingFollow == null)
            {
                return NotFound("Follow relationship not found");
            }
            follow.UserId = userId;
            follow.ArtistId = artistId;
            await _followRepository.UpdateFollows(follow);
            return NoContent();
        }

        // DELETE odata/Follow/User/{userId}/Artist/{artistId}
        [HttpDelete("{userId}/{artistId}")]
        public async Task<ActionResult> Delete(Guid userId, Guid artistId)
        {
            var follow = await _followRepository.GetFollowById(userId, artistId);
            if (follow == null)
            {
                return NotFound("Follow relationship not found");
            }
            await _followRepository.DeleteFollows(userId, artistId);
            return NoContent();
        }
    }
}
