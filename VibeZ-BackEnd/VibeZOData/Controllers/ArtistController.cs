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
    public class ArtistController(IArtistRepository _artistRepository, ILogger<ArtistController> _logger, IMapper _mapper, IAzuriteService _azure) : ControllerBase
    {
        [HttpGet("all", Name = "GetAllArtist")]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtists()
        {
            _logger.LogInformation("Getting all artists");
            var list = await _artistRepository.GetAllArtists();
            var listDTO = list.Select(
                artist => _mapper.Map<Artist, ArtistDTO>(artist));

            _logger.LogInformation($"Retrieved {listDTO.Count()} artists");
            return Ok(listDTO);
        }
        // GET api/<ArtistController>/5
        [HttpGet("{id}", Name = "GetArtistById")]
        public async Task<ActionResult<ArtistDTO>> GetArtistById(Guid id)
        {
            _logger.LogInformation($"Fetching artist with id {id}");
            var artist = await _artistRepository.GetArtistById(id);
            if (artist == null)
            {
                _logger.LogWarning($"Artist with id {id} not found");
                return NotFound("Artist not found");
            }

            var ArtistDto = _mapper.Map<Artist, ArtistDTO>(artist);
            return Ok(artist);
        }
        [HttpGet("Tracks/{artistId}", Name = "GetAllTracksByArtistId")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetAllTracksByArtistId(Guid artistId)
        {
            _logger.LogInformation($"Fetching track with artistId {artistId}");
            var artist = await _artistRepository.GetAllTrackByArtistId(artistId);
            if (artist == null)
            {
                _logger.LogWarning($"Artist with id {artistId} not found");
                return NotFound("Artist not found");
            }

            var trackList = artist.Select(track => _mapper.Map<Track, TrackDTO>(track));
            return Ok(artist);
        }

        // POST api/<ArtistController>
        [HttpPost("CreateArtist", Name = "CreateArtist")]
        public async Task<ActionResult> CreateArtist(string name, string genre,  IFormFile? image, string nation)
        {
            _logger.LogInformation("Creating new Artist");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new Artist creation");
                return BadRequest();
            }
            if (image == null || image.Length == 0)
            {
                _logger.LogWarning("Image file is missing");
                return BadRequest();
            }
            var img = await _azure.UploadFileAsync(image);
            var Artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = name,
                Genre = genre,
                Image = img,
                Nation = nation,
            };
            await _artistRepository.AddArtist(Artist);
            _logger.LogInformation($"Artist created with id {Artist.Id}");
            return CreatedAtRoute("GetArtistById", new { id = Artist.Id }, Artist);
        }


        // PUT api/<ArtistController>/5
        [HttpPut("{id}", Name = "UpdateArtist")]
        public async Task<ActionResult> UpdateArtist(Guid id,string name, string genre, IFormFile? image, string nation)
        {
            _logger.LogInformation($"Updating Artist with id {id}");

            var artist = await _artistRepository.GetArtistById(id);
            if (artist == null)
            {
                _logger.LogWarning($"Artist with id {id} not found for update");
                return NotFound("Artist not found!");
            }
            if (image != null)
            {
                artist.Image = await _azure.UpdateFileAsync(image, artist.Image);
            }
            artist.Name = name;
            artist.Genre = genre;
            artist.Nation = nation;
            artist.UpdateDate  = DateOnly.FromDateTime(DateTime.UtcNow);

            await _artistRepository.UpdateArtist(artist);
            _logger.LogInformation($"Artist with id {id} has been updated");

            return NoContent();
        }

        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}", Name = "DeleteArtist")]
        public async Task<ActionResult> DeleteArtist(Guid id)
        {
            _logger.LogInformation($"Deleting artist with id {id}");

            var artist = await _artistRepository.GetArtistById(id);
            if (artist == null)
            {
                _logger.LogWarning($"Artist with id {id} not found for deletion");
                return NotFound("Artist not found!");
            }

            await _artistRepository.DeleteArtist(artist);
            await _azure.DeleteFileAsync(artist.Image);

            _logger.LogInformation($"Artist with id {id} has been deleted");

            return NoContent();
        }
    }
}
