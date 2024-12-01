using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;
using VibeZDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackPlaylistController(ITracksPlaylistRepository _trackPlaylistRepository, ILogger<TrackPlaylistController> _logger, IMapper _mapper) : ControllerBase
    {
        [HttpGet("all", Name = "GetAllTrackPlayList")]
        public async Task<ActionResult<IEnumerable<TrackPlayListDTO>>> GetAllTrackPlayLists()
        {
            _logger.LogInformation("Getting all trackPlayLists");
            var list = await _trackPlaylistRepository.GetAllTracksPlaylists();
            var listDTO = list.Select(
                trackPlayList => _mapper.Map<Track_Playlist, TrackPlayListDTO>(trackPlayList));

            _logger.LogInformation($"Retrieved {listDTO.Count()} trackPlayLists");
            return Ok(listDTO);
        }

        // GET api/<ArtistController>/5
        [HttpGet("{id}", Name = "GetTrackPlaylistById")]
        public async Task<ActionResult<TrackPlayListDTO>> GetTrackPlaylistById(Guid trackId, Guid playlistId)
        {
            _logger.LogInformation($"Fetching TrackPlaylist with trackId {trackId}, playlistId {playlistId}");
            var trackPlayList = await _trackPlaylistRepository.GetTracksPlaylistById(trackId, playlistId);
            if (trackPlayList == null)
            {
                _logger.LogWarning($"TrackPlaylist with trackId {trackId}, playlistId {playlistId} not found");
                return NotFound("TrackPlaylist not found");
            }

            var trackPlaylistDto = _mapper.Map<Track_Playlist, TrackPlayListDTO>(trackPlayList);
            return Ok(trackPlaylistDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid playlistId, Guid trackId)
        {
            _logger.LogInformation("Creating new playlist_track relationship");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new playlist_track relationship creation");
                return BadRequest(ModelState);
            }

            var playlist_track = new Track_Playlist
            {
                PlaylistId = playlistId,
                TrackId = trackId,
            };

            await _trackPlaylistRepository.AddTracksPlaylist(playlist_track);
            _logger.LogInformation($"playlist_track relationship created between playlistId {playlistId} and trackId {trackId}");
            return Ok();
        }

        //// PUT api/Library_Artist/{libId}/{artistId}
        //[HttpPut("{playlistId}/{trackId}")]
        //public async Task<ActionResult> Put(Guid playlistId, Guid trackId, [FromBody] Track_Playlist track_Playlist)
        //{
        //    _logger.LogInformation($"Updating Library_Artist relationship for LibraryId: {libId} and ArtistId: {artistId}");

        //    var existingLibArtist = await _library_ArtistRepository.GetArtistById(artistId, libId);
        //    if (existingLibArtist == null)
        //    {
        //        _logger.LogWarning($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} not found");
        //        return NotFound();
        //    }

        //    existingLibArtist.LibraryId = updatedLibArtist.LibraryId;
        //    existingLibArtist.ArtistId = updatedLibArtist.ArtistId;

        //    await _library_ArtistRepository.Update(existingLibArtist);
        //    _logger.LogInformation($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} updated");

        //    return Ok(existingLibArtist);
        //}

        // DELETE api/Library_Artist/{libId}/{artistId}
        [HttpDelete("{playlistId}/{trackId}")]
        public async Task<ActionResult> Delete(Guid playlistId, Guid trackId)
        {
            _logger.LogInformation($"Deleting track_playlist relationship for trackId: {trackId} and playlistId: {playlistId}");

            var track_Playlist = await _trackPlaylistRepository.GetTracksPlaylistById(trackId, playlistId);
            if (track_Playlist == null)
            {
                _logger.LogWarning($"track_playlist relationship for playlistId {playlistId} and trackId {trackId} not found");
                return NotFound();
            }

            await _trackPlaylistRepository.DeleteTracksPlaylist(track_Playlist);
            _logger.LogInformation($"track_playlist relationship for playlistId {playlistId} and trackId {trackId} deleted");

            return Ok();
        }
    }
}
