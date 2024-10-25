using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;
using VibeZDTO;
using VibeZOData.Services.Blob;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedBlockedArtistController(IBlockedArtistRepository _blockedArtistRepository, ILogger<BlockedBlockedArtistController> _logger, IMapper _mapper) : ControllerBase
    {
        // GET: api/<BlockedBlockedArtistController>
        [HttpGet("all", Name = "GetAllBlockedArtist")]
        public async Task<ActionResult<IEnumerable<BlockedArtistDTO>>> GetAllBlockedArtists()
        {
            _logger.LogInformation("Getting all blockedArtists");
            var list = await _blockedArtistRepository.GetAllBlockedArtists();
            var listDTO = list.Select(
                BlockedArtist => _mapper.Map<BlockedArtist, BlockedArtistDTO>(BlockedArtist));

            _logger.LogInformation($"Retrieved {listDTO.Count()} BlockedArtists");
            return Ok(listDTO);
        }
        // GET: api/<BlockedBlockedArtistController>
        [HttpGet("{userId}/all", Name = "GetAllBlockedArtistByUserId")]
        public async Task<ActionResult<IEnumerable<BlockedArtistDTO>>> GetAllBlockedArtistsByUserId(Guid id)
        {
            _logger.LogInformation("Getting all blockedArtists by userId");
            var list = await _blockedArtistRepository.GetAllBlockedArtistsByUserId(id);
            var listDTO = list.Select(
                blockedArtist => _mapper.Map<BlockedArtist, BlockedArtistDTO>(blockedArtist));

            _logger.LogInformation($"Retrieved {listDTO.Count()} BlockedArtists");
            return Ok(listDTO);
        }

        // GET api/<BlockedBlockedArtistController>/5
        [HttpGet("{artistId, userId}", Name = "GetBlockedArtistById")]
        public async Task<ActionResult<BlockedArtistDTO>> GetBlockedArtistById(Guid artistId, Guid userId)
        {
            var blockedArtist = await _blockedArtistRepository.GetBlockedArtistById(artistId, userId);
            if (blockedArtist == null)
            {
                _logger.LogWarning($"blockedArtist not found");
                return NotFound("blockedArtist not found");
            }

            var blockedArtistDto = _mapper.Map<BlockedArtist, BlockedArtistDTO>(blockedArtist);
            return Ok(blockedArtist);
        }

        // POST api/<blockedArtistController>
        [HttpPost("CreateBlockedArtist", Name = "CreateBlockedArtist")]
        public async Task<ActionResult> CreateBlockedArtist(Guid userId, Guid artistId)
        {
            _logger.LogInformation("Creating new blockedArtist");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new blockedArtist creation");
                return BadRequest();
            }

            var blockedArtist = new BlockedArtist
            {
                UserId = userId,
                ArtistId = artistId,
                blocked_date = DateTime.UtcNow
            };
            await _blockedArtistRepository.AddBlockedArtist(blockedArtist);
            _logger.LogInformation($"blockedArtist created ");
            return CreatedAtRoute("GetblockedArtistById", new { userId = blockedArtist.UserId, artistId = blockedArtist.ArtistId }, blockedArtist);
        }

        [HttpPut("{id}", Name = "UpdateBlockedArtist")]
        public async Task<ActionResult> UpdateBlockedArtist(Guid artistId, Guid userId)
        {
            _logger.LogInformation($"Updating BlockedArtist with userId {userId}, artistId {artistId}");

            var blockedArtist = await _blockedArtistRepository.GetBlockedArtistById(artistId, userId);
            if (blockedArtist == null)
            {
                _logger.LogWarning($"BlockedArtist  with userId {userId}, artistId {artistId} not found for update");
                return NotFound("BlockedArtist not found!");
            }
            blockedArtist.UserId = userId;
            blockedArtist.ArtistId = artistId;
            blockedArtist.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _blockedArtistRepository.UpdateBlockedArtist(blockedArtist);
            _logger.LogInformation($"BlockedArtist with userId {userId}, artistId {artistId} has been updated");

            return NoContent();
        }
        // DELETE api/<BlockedBlockedArtistController>/5
          // DELETE api/<ArtistController>/5
        [HttpDelete("{id}", Name = "DeleteBlockedArtist")]
        public async Task<ActionResult> DeleteBlockedArtist(Guid userId, Guid artistId)
        {
            _logger.LogInformation($"Deleting BlockedArtist  with userId {userId}, artistId {artistId}");

            var blockedArtist = await _blockedArtistRepository.GetBlockedArtistById(userId, artistId);
            if (blockedArtist == null)
            {
                _logger.LogWarning($"BlockedArtist  with userId {userId}, artistId {artistId} not found for deletion");
                return NotFound("BlockedArtist not found!");
            }

            await _blockedArtistRepository.DeleteBlockedArtist(blockedArtist);

            _logger.LogInformation($"BlockedArtist  with userId {userId}, artistId {artistId} has been deleted");

            return NoContent();
        }
    }
}
