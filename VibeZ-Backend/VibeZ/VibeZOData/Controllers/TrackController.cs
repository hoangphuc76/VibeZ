using AutoMapper;
using Azure;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using VibeZDTO;
using VibeZOData.Services.Blob;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using VibeZOData.Models;
using NuGet.DependencyResolver;

namespace VibeZOData.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class TrackController(ITrackRepository _trackRepository, IMapper _mapper, IAzuriteService _azure, ILogger<TrackController> _logger) : ControllerBase
    {
        // GET: api/<TrackController>
        [HttpGet("all", Name = "GetAllTracks")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetAllTracks()
        {
            _logger.LogInformation("Getting all tracks");
            var list = await _trackRepository.GetAllTracks();
            var listDTO = list.Select(
                track => _mapper.Map<Track, TrackDTO>(track));

            _logger.LogInformation($"Retrieved {listDTO.Count()} tracks");
            return Ok(listDTO);
        }

        // GET api/<TrackController>/5
        [HttpPost("Tracks", Name = "GetTrackByIds")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTrackByIds([FromBody]List<Guid> ids)
        {
            _logger.LogInformation($"Fetching tracks");
            var track = await _trackRepository.GetTrackByIds(ids);
            if (track == null || !track.Any())
            {
                _logger.LogWarning($"Track not found");
                return NotFound("Track not found");
            }

            var listDTO = track.Select(
                 track => _mapper.Map<Track, TrackDTO>(track));
            return Ok(listDTO);
        }

        [HttpPost("GetRecommendations", Name = "GetTrackRecommendations")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTrackRecommendations([FromBody] TrackRecommendationRequest request)
        {
            if (request == null || request.RecentlyPlayedIds == null || !request.RecentlyPlayedIds.Any() || request.ClickedTrackId == Guid.Empty)
            {
                _logger.LogWarning("Invalid request data");
                return BadRequest("Invalid data");
            }

            _logger.LogInformation("Fetching track recommendations");

            // Gọi tới TrackDAO để lấy danh sách gợi ý
            var recommendedTracks = await _trackRepository.GetSongRecommendations(request.RecentlyPlayedIds, request.ClickedTrackId, request.TopN);

            if (recommendedTracks == null || !recommendedTracks.Any())
            {
                _logger.LogWarning("No tracks found for recommendation");
                return NotFound("No tracks found");
            }

            // Chuyển đổi từ Track sang TrackDTO
            var listDTO = recommendedTracks.Select(track => _mapper.Map<Track, TrackDTO>(track));

            return Ok(listDTO);
        }

        [HttpGet("Album/{albumId}", Name = "GetTrackByAlbumId")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTrackByAlbumId(Guid albumId)
        {
            _logger.LogInformation($"Fetching tracks by albumId {albumId}");

            var track = await _trackRepository.GetAllTrackByAlbumId(albumId);
            var trackDTO = track.Select(
                tck => _mapper.Map<Track, TrackDTO>(tck));

            _logger.LogInformation($"Found {trackDTO.Count()} tracks for albumId {albumId}");
            return Ok(trackDTO);
        }
        [HttpGet("{id}", Name = "GetTrackById")]
        public async Task<ActionResult<TrackDTO>> GetTrackById(Guid id)
        {
            _logger.LogInformation($"Fetching track with id {id}");
            var track = await _trackRepository.GetTrackById(id);
            if (track == null)
            {
                _logger.LogWarning($"Track with id {id} not found");
                return NotFound("Track not found");
            }

            var trackDTO = _mapper.Map<Track, TrackDTO>(track);
            return Ok(trackDTO);
        }

        // POST api/<TrackController>
        [HttpPost("upload", Name = "CreateTrack")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult> CreateTrack(Guid? AlbumId,
            Guid? CategoryId, string TrackName, string Lyrics, string Genre, int hour,  int minute, int section, Guid artistId,
            IFormFile path, IFormFile image, IFormFile? trackLRC, IFormFile? songInfoImg)
        {
            var trackUrl = "";
            var songInfo = "";
            _logger.LogInformation("Creating new track");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new track creation");
                return BadRequest();
            }

            if (path == null || path.Length == 0 || image == null || image.Length == 0)
            {
                _logger.LogWarning("Path or image file is missing");
                return BadRequest();
            }
            if (minute < 0 || minute > 59 || section < 0 || section > 59)
            {
                return BadRequest("Invalid time values.");
            }
            if (trackLRC != null)
            {
                trackUrl = await _azure.UploadFileAsync(trackLRC);
            }
            if (songInfoImg != null)
            {
                songInfo = await _azure.UploadFileAsync(songInfoImg);
            }
            var trackTime = new TimeOnly(0, minute, section);
            var pathUrl = await _azure.UploadFileAsync(path);
            var imageUrl = await _azure.UploadFileAsync(image);


            var track = new Track
            {
                TrackId = Guid.NewGuid(),
                AlbumId = AlbumId,
                CategoryId = CategoryId,
                Name = TrackName,
                Genre = Genre,
                Lyrics = Lyrics,
                Path = pathUrl,
                Image = imageUrl,
                Time = trackTime,
                ArtistId = artistId,
                TrackLRC = trackUrl,
                SongInfoImg = songInfo,
            };

            await _trackRepository.AddTrack(track);
            _logger.LogInformation($"Track created with id {track.TrackId}");

            return CreatedAtRoute("GetTrackById", new { id = track.TrackId }, track);
        }

        [HttpPost("updateSong")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult> UpdateTrackToVerify([FromForm]string TrackName, [FromForm]string Lyrics, [FromForm] string Genre, [FromForm] int hour, [FromForm] int minute,
            [FromForm] int section, [FromForm] Guid artistId,
          [FromForm] string path, [FromForm] string image, [FromForm]string  trackLRC)
        {

            _logger.LogInformation("Creating new track");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new track creation");
                return BadRequest();
            }

          
            var trackTime = new TimeOnly(0, minute, section);
        


            var track = new Track
            {
                TrackId = Guid.NewGuid(),
                Name = TrackName,
                Genre = Genre,
                Lyrics = Lyrics,
                Path = path,
                Image = image,
                Time = trackTime,
                ArtistId = artistId,
                TrackLRC = trackLRC,
            };

            await _trackRepository.AddTrack(track);
            _logger.LogInformation($"Track created with id {track.TrackId}");

            return CreatedAtRoute("GetTrackById", new { id = track.TrackId }, track);
        }

        // PUT api/<TrackController>/5
        [HttpPut("{id}", Name = "UpdateTrack")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdateTrack(Guid id, Guid? AlbumId, 
             Guid? CategoryId, string TrackName, string Lyrics, string Genre, int hour, int minute, int section, Guid artistId,
            IFormFile? path, IFormFile? image, IFormFile? trackLRC, IFormFile? songInfoImg)
        {
            var trackUrl = "";
            var songInfo = "";
            _logger.LogInformation($"Updating track with id {id}");

            var track = await _trackRepository.GetTrackById(id);
            if (track == null)
            {
                _logger.LogWarning($"Track with id {id} not found for update");
                return NotFound("Track not found!");
            }

            if (path != null)
            {
                track.Path = await _azure.UpdateFileAsync(path, track.Path);
            }
            if (image != null)
            {
                track.Image = await _azure.UpdateFileAsync(image, track.Image);
            }
            if (minute < 0 || minute > 59)
            {
                return BadRequest("Invalid time values.");
            }
            if (trackLRC != null || trackLRC.Length > 0)
            {
                trackUrl = await _azure.UploadFileAsync(trackLRC);
            }
            if (songInfoImg != null)
            {
                songInfo = await _azure.UploadFileAsync(songInfoImg);
            }
            var trackTime = new TimeOnly(0, minute, section);
            track.AlbumId = AlbumId;
            track.CategoryId = CategoryId;
            track.Name = TrackName;
            track.Lyrics = Lyrics;
            track.Genre = Genre;
            track.Time = trackTime;
            track.UpdateDate = DateOnly.FromDateTime(DateTime.UtcNow);
            track.ArtistId = artistId;
            track.TrackLRC = trackUrl;
            track.SongInfoImg = songInfo;
            await _trackRepository.UpdateTrack(track);
            _logger.LogInformation($"Track with id {id} has been updated");

            return NoContent();
        }

        [HttpPut("/Song/{id}", Name = " UpdateSongInfoAndTrackLRC")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdateSongInfoAndTrackLRC(Guid id, IFormFile? trackLRC, IFormFile? songInfoImg)
        {
            var trackUrl = "";
            var songInfo = "";
            _logger.LogInformation($"Updating track with id {id}");
            var track = await _trackRepository.GetTrackById(id);
            if (track == null)
            {
                _logger.LogWarning($"Track with id {id} not found for update");
                return NotFound("Track not found!");
            }
            if (trackLRC != null || trackLRC.Length > 0)
            {
                trackUrl = await _azure.UploadFileAsync(trackLRC);
            }
            if (songInfoImg != null)
            {
                songInfo = await _azure.UploadFileAsync(songInfoImg);
            }
            track.TrackLRC = trackUrl;
            track.SongInfoImg = songInfo;
            await _trackRepository.UpdateTrack(track);
            _logger.LogInformation($"Track with id {id} has been updated");

            return NoContent();
        }

        // DELETE api/<TrackController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTracks(Guid id)
        {
            _logger.LogInformation($"Deleting track with id {id}");

            var track = await _trackRepository.GetTrackById(id);
            if (track == null)
            {
                _logger.LogWarning($"Track with id {id} not found for deletion");
                return NotFound("Track not found!");
            }

            await _trackRepository.DeleteTrack(track);
            await _azure.DeleteFileAsync(track.Path);
            await _azure.DeleteFileAsync(track.Image);

            _logger.LogInformation($"Track with id {id} has been deleted");

            return NoContent();
        }

        [HttpPut("UpdateListener/{id}")]
        public async Task<ActionResult> UpdateListener(Guid id)
        {
            _logger.LogInformation($"Updating listener for track with id {id}");
            var existTrack = await _trackRepository.GetTrackById(id);
            if (existTrack == null)
            {
                _logger.LogWarning($"Track with id {id} not found for listener update");
                return NotFound("Track not found");
            }
            await _trackRepository.UpdateListener(existTrack);
            _logger.LogInformation($"Listener updated for track with id {id}");

            return NoContent();
        }
    }
}
