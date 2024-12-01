using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VibeZDTO;

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistDashboardController : ControllerBase
    {
        private readonly IArtistDashboarRepository artistDashboarRepository;
        private readonly ILogger<ArtistDashboardController> _logger;
        private readonly IMapper _mapper;

        public ArtistDashboardController(IArtistDashboarRepository artistDashboarRepository, ILogger<ArtistDashboardController> logger, IMapper mapper)
        {
            this.artistDashboarRepository = artistDashboarRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<ArtistDashboardController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Get() called - returning default values.");
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArtistDashboardController>/TotalFollow
        [HttpGet("TotalFollow")]
        public async Task<ActionResult<int>> GetTotalFollow(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            _logger.LogInformation("GetTotalFollow called with ArtistId: {ArtistId}, StartDate: {StartDate}, EndDate: {EndDate}", artistId, startDate, endDate);
            var result = await artistDashboarRepository.GetAllFollowById(artistId, startDate, endDate);
            _logger.LogInformation("GetTotalFollow result: {Result}", result);
            return Ok(result);
        }

        // GET api/<ArtistDashboardController>/TotalUnfollow
        [HttpGet("TotalUnfollow")]
        public async Task<ActionResult<int>> GetTotalUnFollow(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            _logger.LogInformation("GetTotalUnFollow called with ArtistId: {ArtistId}, StartDate: {StartDate}, EndDate: {EndDate}", artistId, startDate, endDate);
            var result = await artistDashboarRepository.GetAllUnFollowById(artistId, startDate, endDate);
            _logger.LogInformation("GetTotalUnFollow result: {Result}", result);
            return Ok(result);
        }

        // GET api/<ArtistDashboardController>/TotalListener
        [HttpGet("TotaListener")]
        public async Task<ActionResult<int>> GetTotalListener(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            _logger.LogInformation("GetTotalListener called with ArtistId: {ArtistId}, StartDate: {StartDate}, EndDate: {EndDate}", artistId, startDate, endDate);
            var result = await artistDashboarRepository.CountTotalListenerByArtist(artistId, startDate, endDate);
            _logger.LogInformation("GetTotalListener result: {Result}", result);
            return Ok(result);
        }

        // GET api/<ArtistDashboardController>/TotalSavePlaylist
        [HttpGet("TotaSavePlaylist")]
        public async Task<ActionResult<int>> GetTotalSavedPlaylist(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            _logger.LogInformation("GetTotalSavedPlaylist called with ArtistId: {ArtistId}, StartDate: {StartDate}, EndDate: {EndDate}", artistId, startDate, endDate);
            var result = await artistDashboarRepository.GetTotalSavedTrack(artistId, startDate, endDate);
            _logger.LogInformation("GetTotalSavedPlaylist result: {Result}", result);
            return Ok(result);
        }

        // GET api/<ArtistDashboardController>/TopSong
        [HttpGet("TopSong")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTopSongs(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            _logger.LogInformation("GetTopSongs called with ArtistId: {ArtistId}, StartDate: {StartDate}, EndDate: {EndDate}", artistId, startDate, endDate);
            var result = await artistDashboarRepository.GetTop10Songs(artistId, startDate, endDate);
            var resultDto = result.Select(track => _mapper.Map<Track, TrackDTO>(track)).ToList();
            _logger.LogInformation("GetTopSongs result count: {Count}", resultDto.Count);
            return Ok(resultDto);
        }
    }
}
