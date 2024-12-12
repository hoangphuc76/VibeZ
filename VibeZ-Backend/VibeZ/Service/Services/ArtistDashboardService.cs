using BusinessObjects;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ArtistDashboardService : IArtistDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArtistDashboardService> _logger;

        public ArtistDashboardService(IUnitOfWork unitOfWork, ILogger<ArtistDashboardService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> GetAllFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return await _unitOfWork.Follows.GetAllFollowById(artistId, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting follows for artist {artistId}");
                throw;
            }
        }

        public async Task<int> GetAllUnFollowById(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return await _unitOfWork.Follows.GetAllUnFollowById(artistId, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting unfollows for artist {artistId}");
                throw;
            }
        }

        public async Task<int> CountTotalListenerByArtist(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return await _unitOfWork.Tracks.CountTotalListenerByArtist(artistId, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error counting total listeners for artist {artistId}");
                throw;
            }
        }

        public async Task<IEnumerable<Track>> GetTop10Songs(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return await _unitOfWork.Tracks.GetTop10Songs(artistId, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting top 10 songs for artist {artistId}");
                throw;
            }
        }

        public async Task<int> GetTotalSavedTrack(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return await _unitOfWork.TrackPlaylists.GetTotalSavedTrack(artistId, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting total saved tracks for artist {artistId}");
                throw;
            }
        }
    }
}
