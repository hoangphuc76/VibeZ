using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Library_ArtistController(ILibrary_ArtistRepository _library_ArtistRepository, ILogger<Library_ArtistController> _logger) : ControllerBase
    {

        [HttpGet("{libId}/{artistId}")]
        public async Task<ActionResult> Get(Guid libId, Guid artistId)
        {
            _logger.LogInformation($"Fetching Library_Artist relationship for LibraryId: {libId} and ArtistId: {artistId}");

            var existingLibArtist = await _library_ArtistRepository.GetArtistById(artistId, libId);
            if (existingLibArtist == null)
            {
                _logger.LogWarning($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} not found");
                return NotFound();
            }

            return Ok();
        }

        // POST api/Library_Artist/Create
        [HttpPost("Follow")]
        public async Task<ActionResult> Create([FromForm]Guid libId, [FromForm]Guid artistId)
        {
            _logger.LogInformation("Creating new Library_Artist relationship");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for new Library_Artist relationship creation");
                return BadRequest(ModelState);
            }

            var libraryArtist = new Library_Artist
            {
                LibraryId = libId,
                ArtistId = artistId,
            };

            await _library_ArtistRepository.Add(libraryArtist);
            _logger.LogInformation($"Library_Artist relationship created between LibraryId {libId} and ArtistId {artistId}");
            return Ok();
        }

        // PUT api/Library_Artist/{libId}/{artistId}
        [HttpPut("{libId}/{artistId}")]
        public async Task<ActionResult> Put(Guid libId, Guid artistId, [FromBody] Library_Artist updatedLibArtist)
        {
            _logger.LogInformation($"Updating Library_Artist relationship for LibraryId: {libId} and ArtistId: {artistId}");

            var existingLibArtist = await _library_ArtistRepository.GetArtistById(artistId, libId);
            if (existingLibArtist == null)
            {
                _logger.LogWarning($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} not found");
                return NotFound();
            }

            existingLibArtist.LibraryId = updatedLibArtist.LibraryId;
            existingLibArtist.ArtistId = updatedLibArtist.ArtistId;

            await _library_ArtistRepository.Update(existingLibArtist);
            _logger.LogInformation($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} updated");

            return Ok(existingLibArtist);
        }

        // DELETE api/Library_Artist/{libId}/{artistId}
        [HttpDelete("{libId}/{artistId}")]
        public async Task<ActionResult> Delete(Guid libId, Guid artistId)
        {
            _logger.LogInformation($"Deleting Library_Artist relationship for LibraryId: {libId} and ArtistId: {artistId}");

            var existingLibArtist = await _library_ArtistRepository.GetArtistById(artistId, libId);
            if (existingLibArtist == null)
            {
                _logger.LogWarning($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} not found");
                return NotFound();
            }

            await _library_ArtistRepository.Delete(artistId, libId);
            _logger.LogInformation($"Library_Artist relationship for LibraryId {libId} and ArtistId {artistId} deleted");

            return Ok();
        }
    }
}
