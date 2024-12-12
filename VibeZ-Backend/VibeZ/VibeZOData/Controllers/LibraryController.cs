using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.UnitOfWork;
using VibeZDTO;
using VibeZOData.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController(IUnitOfWork _unitOfWork, ILogger<LibraryController> _logger, IMapper _mapper) : ControllerBase
    {
        [HttpGet("{libraryId}/playlists")]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylistsByLibraryId(Guid libraryId)
        {
            var playlists = await _unitOfWork.Libraries.GetPlaylistsByLibraryId(libraryId);
            if (playlists == null)
            {
                return NotFound($"No playlists found for library with ID {libraryId}");
            }

            var playlistDTOs = _mapper.Map<IEnumerable<PlaylistDTO>>(playlists);
            return Ok(playlists);
        }
        [HttpGet("{libraryId}/artists")]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtistsByLibraryId(Guid libraryId)
        {
            var artists = await _unitOfWork.Libraries.GetArtistByLibraryId(libraryId);
            if (artists == null)
            {
                return NotFound($"No artists found for library with ID {libraryId}");
            }

            var artistDTOs = _mapper.Map<IEnumerable<ArtistDTO>>(artists);
            return Ok(artistDTOs);
        }

        [HttpGet("{libraryId}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbumsByLibraryId(Guid libraryId)
        {
            var albums = await _unitOfWork.Libraries.GetAlbumsByLibraryId(libraryId);
            if (albums == null)
            {
                return NotFound($"No albums found for library with ID {libraryId}");
            }

            var albumDTOs = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
            return Ok(albumDTOs);
        }
        

        [HttpGet("{userId}")]
        public async Task<ActionResult<Library>> GetLibraryByUserId(Guid userId)
        {
          var lib = await _unitOfWork.Libraries.GetById(userId);
            if (lib == null)
            {
                return NotFound($"No Library");
            }
            return Ok(lib);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<LibraryDTO>>> GetAll()
        {
            var lib = await _unitOfWork.Libraries.GetAll();
            if (lib == null)
            {
                return NotFound($"No Library");
            }
            var libDto = lib.Select(library => _mapper.Map<Library, Library>(library));
            return Ok(libDto);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] LibraryDTO library)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var lib = new Library
            { 
                UserId = library.UserId,
            };

            await _unitOfWork.Libraries.Add(lib);
            await _unitOfWork.Complete();

            return Ok();
        }

    }
}
