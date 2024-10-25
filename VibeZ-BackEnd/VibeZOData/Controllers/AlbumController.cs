using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using VibeZDTO;
using VibeZOData.Services.Blob;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController(IAlbumRepository _albumRepository, ILogger<AlbumController> _logger, IMapper _mapper, IAzuriteService _azure) : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet("{artistId}/all", Name = "GetAllAlbumByArtistId")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAllAlbumByArtistId(Guid artistId)
        {
            _logger.LogInformation("Getting all album");
            var list = await _albumRepository.GetAllAlbumsByArtistId(artistId);
            var listDto = list.Select(album => _mapper.Map<Album, AlbumDTO>(album));
            _logger.LogInformation($"Retrieved {listDto.Count()} album");
            return Ok(listDto);
        }

        [HttpGet("all", Name = "GetAllAlbums")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAllalbums()
        {
            _logger.LogInformation("Getting all albums");
            var list = await _albumRepository.GetAllAlbums();
            var listDTO = list.Select(
                album => _mapper.Map<Album, AlbumDTO>(album));

            _logger.LogInformation($"Retrieved {listDTO.Count()} albums");
            return Ok(listDTO);
        }
      
        // GET api/<ValuesController>/5
        // GET api/<albumController>/5
        [HttpGet("{id}", Name = "GetAlbumById")]
        public async Task<ActionResult<AlbumDTO>> GetAlbumById(Guid id)
        {
            _logger.LogInformation($"Fetching album with id {id}");
            var album = await _albumRepository.GetAlbumById(id);
            if (album == null)
            {
                _logger.LogWarning($"album with id {id} not found");
                return NotFound("album not found");
            }

            var albumDto = _mapper.Map<Album, AlbumDTO>(album);
            return Ok(album);
        }

        // POST api/<ValuesController>
        [HttpPost("CreateAlbum", Name = "CreateAlbum")]
        public async Task<ActionResult> CreateAlbum(Guid? artistId, string name, int yy, int mm, int dd, IFormFile? image, string nation)
        {
            _logger.LogInformation("Creating new album");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new album creation");
                return BadRequest();
            }
            if (image == null || image.Length == 0)
            {
                _logger.LogWarning("Image file is missing");
                return BadRequest();
            }
            if (yy < 0 || yy > 2024 || mm < 0 || mm > 12 || dd < 0 || dd > 31)
            {
                return BadRequest("Invalid time values.");
            }
            var dateRelease = new DateOnly(yy, mm, dd);
            var img = await _azure.UploadFileAsync(image);
            var album = new Album
            {
                Id = Guid.NewGuid(),
                Name = name,
                Image = img,
                DateOfRelease = dateRelease,
                Nation = nation,
                ArtistId = artistId
            };
            await _albumRepository.AddAlbum(album);
            _logger.LogInformation($"album created with id {album.Id}");
            return CreatedAtRoute("GetAlbumById", new { id = album.Id }, album);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}", Name = "UpdateAlbum")]
        public async Task<ActionResult> Updatealbum(Guid id,string name, int yy, int mm, int dd,  IFormFile? image, string nation)
        {
            _logger.LogInformation($"Updating album with id {id}");

            var album = await _albumRepository.GetAlbumById(id);
            if (album == null)
            {
                _logger.LogWarning($"album with id {id} not found for update");
                return NotFound("album not found!");
            }
            if (yy < 0 || yy > 2024 || mm <0 || mm >12 || dd <0 || dd > 31)
            {
                return BadRequest("Invalid time values.");
            }
            var dateRelease = new DateOnly(yy, mm, dd);
            if (image != null)
            {
                album.Image = await _azure.UpdateFileAsync(image, album.Image);
            }
            album.Name = name;
            album.DateOfRelease = dateRelease;
            album.Nation = nation;
            album.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _albumRepository.UpdateAlbum(album);
            _logger.LogInformation($"album with id {id} has been updated");

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        // DELETE api/<albumController>/5
        [HttpDelete("{id}", Name = "DeleteAlbum")]
        public async Task<ActionResult> DeleteAlbum(Guid id)
        {
            _logger.LogInformation($"Deleting album with id {id}");

            var album = await _albumRepository.GetAlbumById(id);
            if (album == null)
            {
                _logger.LogWarning($"album with id {id} not found for deletion");
                return NotFound("album not found!");
            }

            await _albumRepository.DeleteAlbum(album);
            await _azure.DeleteFileAsync(album.Image);

            _logger.LogInformation($"album with id {id} has been deleted");

            return NoContent();
        }
    }
}
