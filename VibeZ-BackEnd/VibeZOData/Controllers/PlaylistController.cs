using AutoMapper;
using BusinessObjects;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;
using System.IO;
using VibeZDTO;
using VibeZOData.Services.Blob;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class PlaylistController(IPlaylistRepository _playlistRepository, ILogger<PlaylistController> _logger, IMapper _mapper, IAzuriteService _azure) : ControllerBase
    {
        [HttpGet("Tracks/{playlistId}", Name = "GetTracksByPlaylistId")]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetTracksByPlaylistId(Guid playlistId)
        {
            _logger.LogInformation("Getting all tracks in playlist ");
            var list = await _playlistRepository.GetTracksByPlaylistId(playlistId);
            var listDto = list.Select(track => _mapper.Map<Track, TrackDTO>(track));
            _logger.LogInformation($"Retrieved {listDto.Count()} tracks");
            return Ok(listDto);
        }

        // GET: api/<ValuesController>
        [HttpGet("{userId}/all", Name = "GetAllPlaylistByUserId")]
    public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetAllPlaylistByUserId(Guid userId)
    {
        _logger.LogInformation("Getting all playlist");
        var list = await _playlistRepository.GetAllPlaylistByUserId(userId);
        var listDto = list.Select(playlist => _mapper.Map<Playlist, PlaylistDTO>(playlist));
        _logger.LogInformation($"Retrieved {listDto.Count()} playlist");
        return Ok(listDto);
    }

    [HttpGet("all", Name = "GetAllPlaylists")]
    public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetAllplaylists()
    {
        _logger.LogInformation("Getting all playlists");
        var list = await _playlistRepository.GetAllPlaylists();
        var listDTO = list.Select(
            playlist => _mapper.Map<Playlist, PlaylistDTO>(playlist));

        _logger.LogInformation($"Retrieved {listDTO.Count()} playlists");
        return Ok(listDTO);
    }
    // GET api/<ValuesController>/5
    // GET api/<playlistController>/5

    [HttpGet("{id}", Name = "GetPlaylistById")]
    public async Task<ActionResult<PlaylistDTO>> GetPlaylistById(Guid id)
    {
        _logger.LogInformation($"Fetching playlist with id {id}");
        var playlist = await _playlistRepository.GetPlaylistById(id);
        if (playlist == null)
        {
            _logger.LogWarning($"playlist with id {id} not found");
            return NotFound("playlist not found");
        }

        var playlistDto = _mapper.Map<Playlist, PlaylistDTO>(playlist);
        return Ok(playlist);
    }

    // POST api/<ValuesController>
    [HttpPost("CreatePlaylist")]
    public async Task<ActionResult> CreatePlaylist([FromForm]string name, 
        [FromForm]string description , 
        [FromForm]string createBy, 
        [FromForm]IFormFile image, 
        [FromForm]Guid? userId)
    {
        _logger.LogInformation("Creating new playlist");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for new playlist creation");
            return BadRequest();
        }
        if (image == null || image.Length == 0)
        {
            _logger.LogWarning("Image file is missing");
            return BadRequest();
        }
        var img = await _azure.UploadFileAsync(image);
        var playlist = new Playlist
        {
            PlaylistId = Guid.NewGuid(),
            Name = name,
            Description = description,
            createBy = createBy,
            Image = img,
            UserId = userId,
        };
        await _playlistRepository.AddPlaylist(playlist);
        _logger.LogInformation($"Playlist created with id {playlist.PlaylistId}");
        return CreatedAtRoute("GetPlaylistById", new { id = playlist.PlaylistId }, playlist);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}", Name = "UpdatePlaylist")]
    public async Task<ActionResult> UpdatePlaylist(Guid id, string name, string description ,string createBy, IFormFile? image)
    {
        _logger.LogInformation($"Updating playlist with id {id}");

        var playlist = await _playlistRepository.GetPlaylistById(id);
        if (playlist == null)
        {
            _logger.LogWarning($"playlist with id {id} not found for update");
            return NotFound("playlist not found!");
        }

        if (image != null)
        {
            playlist.Image = await _azure.UpdateFileAsync(image, playlist.Image);
        }
        if (image != null)
        {
            playlist.Image = await _azure.UpdateFileAsync(image, playlist.Image);
        }

            playlist.Name = name;
            playlist.Description = description;
            playlist.createBy = createBy;
            playlist.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _playlistRepository.UpdatePlaylist(playlist);
            _logger.LogInformation($"playlist with id {id} has been updated");

            return NoContent();
    }

    // DELETE api/<ValuesController>/5
    // DELETE api/<playlistController>/5
    [HttpDelete("{id}", Name = "DeletePlaylist")]
    public async Task<ActionResult> DeletePlaylist(Guid id)
    {
        _logger.LogInformation($"Deleting playlist with id {id}");

        var playlist = await _playlistRepository.GetPlaylistById(id);
        if (playlist == null)
        {
            _logger.LogWarning($"playlist with id {id} not found for deletion");
            return NotFound("playlist not found!");
        }

        await _playlistRepository.DeletePlaylist(playlist);
        await _azure.DeleteFileAsync(playlist.Image);

        _logger.LogInformation($"playlist with id {id} has been deleted");

        return NoContent();
    }
}
    
}
