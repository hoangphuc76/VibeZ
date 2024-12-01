using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using System.Drawing;
using System.IO;
using VibeZDTO;
using VibeZOData.Services.Blob;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistPendingController(IArtistPendingRepository _artistPendingRepository, ILogger<ArtistPendingController> _logger, IMapper _mapper, IAzuriteService _azuriteService) : ControllerBase
    {
        // GET: api/ArtistPending
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistPendingDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all ArtistPending records.");

            var artists = await _artistPendingRepository.GetAll();
            var artistDTOs = artists.Select(artist => _mapper.Map<ArtistPending, ArtistPendingDTO>(artist));

            _logger.LogInformation("Fetched {Count} records.", artistDTOs.Count());

            return Ok(artistDTOs);
        }

        // GET: api/ArtistPending/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistPendingDTO>> GetById(Guid id)
        {
            _logger.LogInformation("Fetching ArtistPending record with ID: {Id}", id);

            var artist = await _artistPendingRepository.GetById(id);
            if (artist == null)
            {
                _logger.LogWarning("ArtistPending record with ID: {Id} not found.", id);
                return NotFound();
            }

            var artistDTO = _mapper.Map<ArtistPending, ArtistPendingDTO>(artist);
            return Ok(artistDTO);
        }

        // POST: api/ArtistPending
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]string artistName, [FromForm]string email, [FromForm] Guid userId, [FromForm] IFormFile image, [FromForm] IFormFile imgBackground
            , [FromForm] IFormFile lyrics, [FromForm] IFormFile lyricLRC, [FromForm] IFormFile song, [FromForm] string genre, [FromForm] int minute, [FromForm] int second,
            [FromForm] IFormFile songImg, [FromForm] string songName, [FromForm] string nation
            )
        {
            _logger.LogInformation("Adding a new ArtistPending record.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new track creation");
                return BadRequest();
            }

            if (song == null || song.Length == 0 || image == null || image.Length == 0 || imgBackground == null || imgBackground.Length == 0
                || lyricLRC == null || lyricLRC.Length == 0 || lyrics == null || lyrics.Length ==0 || songImg == null || songImg.Length == 0
                )
            {
                _logger.LogWarning("Path or image file is missing");
                return BadRequest();
            }
            var imgUrl = await _azuriteService.UploadFileAsync(image);
            var imgBackgroundUrl = await _azuriteService.UploadFileAsync(imgBackground);
            var lyricUrl = await _azuriteService.UploadFileAsync(lyrics);
            var lyricLRcUrl = await _azuriteService.UploadFileAsync( lyricLRC);
            var songUrl = await _azuriteService.UploadFileAsync(song);
            var songimgUrl = await _azuriteService.UploadFileAsync (songImg);

            var artistPending = new ArtistPending
            {
                ArtistName = artistName,
                Email = email,
                Genre = genre,
                Image = imgUrl,
                Song = songUrl,
                ImgBackground = imgBackgroundUrl,
                LyricLRC = lyricLRcUrl,
                Lyrics = lyricUrl,
                UserId = userId,
                Hour = 0,
                Minute = minute,
                Section = second,
                SongImg = songimgUrl,
                SongName = songName,
                Nation = nation,
                CreateDate = DateOnly.FromDateTime(DateTime.Now)

            };
            await _artistPendingRepository.Add(artistPending);

            _logger.LogInformation("Added new ArtistPending record with ID: {Id}", artistPending.Id);

            return CreatedAtAction(nameof(GetById), new { id = artistPending.Id }, artistPending);
        }

        // PUT: api/ArtistPending/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromForm] string artistName, [FromForm] string email, [FromForm] Guid userId, [FromForm] IFormFile image, [FromForm] IFormFile imgBackground
            , [FromForm] IFormFile lyrics, [FromForm] IFormFile lyricLRC, [FromForm] IFormFile song, [FromForm] string genre, [FromForm] IFormFile songImg)
        {
            _logger.LogInformation("Updating ArtistPending record with ID: {Id}", id);

            var artistPendingDTO = await _artistPendingRepository.GetById(id);
            if (artistPendingDTO == null)
            {
                _logger.LogWarning("ArtistPending ID mismatch.");
                return BadRequest("Artist ID mismatch.");
            }
            if (song == null || song.Length == 0 || image == null || image.Length == 0 || imgBackground == null || imgBackground.Length == 0
               || lyricLRC == null || lyricLRC.Length == 0 || lyrics == null || lyrics.Length == 0 || songImg == null || songImg.Length > 0
               )
            {
                _logger.LogWarning("Path or image file is missing");
                return BadRequest();
            }

            var imgUrl = await _azuriteService.UpdateFileAsync(image, artistPendingDTO.Image);
            var imgBackgroundUrl = await _azuriteService.UpdateFileAsync(imgBackground, artistPendingDTO.ImgBackground);
            var lyricUrl = await _azuriteService.UpdateFileAsync(lyrics, artistPendingDTO.Lyrics);
            var lyricLRcUrl = await _azuriteService.UpdateFileAsync(lyricLRC, artistPendingDTO.LyricLRC);
            var songUrl = await _azuriteService.UpdateFileAsync(song, artistPendingDTO.Song);
            var songImgUrl = await _azuriteService.UpdateFileAsync(songImg, artistPendingDTO.SongImg);

            artistPendingDTO.ArtistName = artistName;
            artistPendingDTO.Lyrics = lyricUrl;
            artistPendingDTO.Email = email;
            artistPendingDTO.Genre = genre;
            artistPendingDTO.Image = imgUrl;
            artistPendingDTO.ImgBackground = imgBackgroundUrl;
            artistPendingDTO.Song = songUrl;
            artistPendingDTO.UserId = userId;
            artistPendingDTO.LyricLRC = lyricLRcUrl;
            artistPendingDTO.SongImg = songImgUrl;
            artistPendingDTO.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
             await _artistPendingRepository.Update(artistPendingDTO);

            _logger.LogInformation("Updated ArtistPending record with ID: {Id}", id);

            return NoContent();
        }

        // DELETE: api/ArtistPending/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting ArtistPending record with ID: {Id}", id);

            var artist = await _artistPendingRepository.GetById(id);
            if (artist == null)
            {
                _logger.LogWarning("ArtistPending record with ID: {Id} not found.", id);
                return NotFound();
            }

            await _artistPendingRepository.Delete(artist);

            _logger.LogInformation("Deleted ArtistPending record with ID: {Id}", id);

            return NoContent();
        }
    }
}
