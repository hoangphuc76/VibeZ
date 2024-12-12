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
    public class ArtistPendingService : IArtistPendingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArtistPendingService> _logger;

        public ArtistPendingService(IUnitOfWork unitOfWork, ILogger<ArtistPendingService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddArtistPending(ArtistPending artistPending)
        {
            try
            {
                await _unitOfWork.ArtistPending.Add(artistPending);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding artist pending");
                throw;
            }
        }

        public async Task UpdateArtistPending(ArtistPending artistPending)
        {
            try
            {
                await _unitOfWork.ArtistPending.Update(artistPending);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating artist pending with ID: {artistPending.Id}");
                throw;
            }
        }

        public async Task DeleteArtistPending(ArtistPending artistPending)
        {
            try
            {
                await _unitOfWork.ArtistPending.Delete(artistPending);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting artist pending with ID: {artistPending.Id}");
                throw;
            }
        }

        public async Task<IEnumerable<ArtistPending>> GetAllArtistPending()
        {
            try
            {
                return await _unitOfWork.ArtistPending.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all artist pending");
                throw;
            }
        }

        public async Task<ArtistPending> GetArtistPendingById(Guid id)
        {
            try
            {
                return await _unitOfWork.ArtistPending.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting artist pending with ID: {id}");
                throw;
            }
        }
    }
}
