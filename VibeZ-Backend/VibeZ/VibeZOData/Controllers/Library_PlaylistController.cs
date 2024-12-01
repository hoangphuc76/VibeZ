using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Library_PlaylistController(ILibrary_PlaylistRepository _library_PlaylistRepository, ILogger<Library_PlaylistController> _logger) : ControllerBase
    {
        // GET: api/<Library_PlaylistController>


        // POST api/Library_Playlist/Create
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm]Guid libId, [FromForm]Guid playId)
        {
            _logger.LogInformation("Creating new Library_Playlist relationship");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new Library_Playlist relationship creation");
                return BadRequest(ModelState);
            }

            var libraryPlaylist = new Library_Playlist
            {
                LibraryId = libId,
                PlaylistId = playId,
            };

            await _library_PlaylistRepository.Add(libraryPlaylist);
            _logger.LogInformation($"Library_Playlist relationship created between LibraryId {libId} and PlaylistId {playId}");
            return Ok();
        }

        // PUT api/Library_Playlist/{libId}/{playlistId}
        [HttpPut("{libId}/{playlistId}")]
        public async Task<ActionResult> Put(Guid libId, Guid playlistId, [FromBody] Library_Playlist updatedLibPlaylist)
        {
            _logger.LogInformation($"Updating Library_Playlist relationship for LibraryId: {libId} and PlaylistId: {playlistId}");

            var existingLibPlaylist = await _library_PlaylistRepository.GetLibraryPlaylistById(libId, playlistId);
            if (existingLibPlaylist == null)
            {
                _logger.LogWarning($"Library_Playlist relationship for LibraryId {libId} and PlaylistId {playlistId} not found");
                return NotFound();
            }

            existingLibPlaylist.LibraryId = updatedLibPlaylist.LibraryId;
            existingLibPlaylist.PlaylistId = updatedLibPlaylist.PlaylistId;

            await _library_PlaylistRepository.Update(existingLibPlaylist);
            _logger.LogInformation($"Library_Playlist relationship for LibraryId {libId} and PlaylistId {playlistId} updated");

            return Ok(existingLibPlaylist);
        }

        // DELETE api/Library_Playlist/{libId}/{playlistId}
        [HttpDelete("{libId}/{playlistId}")]
        public async Task<ActionResult> Delete(Guid libId, Guid playlistId)
        {
            _logger.LogInformation($"Deleting Library_Playlist relationship for LibraryId: {libId} and PlaylistId: {playlistId}");
            await _library_PlaylistRepository.Delete(libId, playlistId);
            _logger.LogInformation($"Library_Playlist relationship for LibraryId {libId} and PlaylistId {playlistId} deleted");

            return Ok();
        }
    }
}
