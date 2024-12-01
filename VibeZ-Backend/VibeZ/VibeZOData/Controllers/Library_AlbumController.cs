using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Library_AlbumController(ILibrary_AlbumRepository _library_AlbumRepository, ILogger<Library_AlbumController> _logger) : ControllerBase
    {
        // POST api/Library_Album/Create
        [HttpPost]
        public async Task<ActionResult> Create(Guid libId, Guid albumId)
        {
            _logger.LogInformation("Creating new Library_Album relationship");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new Library_Album relationship creation");
                return BadRequest(ModelState);
            }

            var libraryAlbum = new Library_Album
            {
                LibraryId = libId,
                AlbumId = albumId,
            };

            await _library_AlbumRepository.Add(libraryAlbum);
            _logger.LogInformation($"Library_Album relationship created between LibraryId {libId} and AlbumId {albumId}");
            return Ok();
        }

        // PUT api/Library_Album/{libId}/{albumId}
        [HttpPut("{libId}/{albumId}")]
        public async Task<ActionResult> Put(Guid libId, Guid albumId, [FromBody] Library_Album updatedLibAlbum)
        {
            _logger.LogInformation($"Updating Library_Album relationship for LibraryId: {libId} and AlbumId: {albumId}");

            var existingLibAlbum = await _library_AlbumRepository.GetLibraryAlbumById(albumId, libId);
            if (existingLibAlbum == null)
            {
                _logger.LogWarning($"Library_Album relationship for LibraryId {libId} and AlbumId {albumId} not found");
                return NotFound();
            }

            existingLibAlbum.LibraryId = updatedLibAlbum.LibraryId;
            existingLibAlbum.AlbumId = updatedLibAlbum.AlbumId;

            await _library_AlbumRepository.Update(existingLibAlbum);
            _logger.LogInformation($"Library_Album relationship for LibraryId {libId} and AlbumId {albumId} updated");

            return Ok(existingLibAlbum);
        }

        // DELETE api/Library_Album/{libId}/{albumId}
        [HttpDelete("{libId}/{albumId}")]
        public async Task<ActionResult> Delete(Guid libId, Guid albumId)
        {
            _logger.LogInformation($"Deleting Library_Album relationship for LibraryId: {libId} and AlbumId: {albumId}");
            await _library_AlbumRepository.Delete(albumId,libId);
            _logger.LogInformation($"Library_Album relationship for LibraryId {libId} and AlbumId {albumId} deleted");

            return Ok();
        }
    }
}

