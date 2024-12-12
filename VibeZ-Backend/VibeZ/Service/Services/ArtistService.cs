using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    using BusinessObjects;
    using global::Service.IServices;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Repositories.UnitOfWork;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VibeZDTO;

    namespace Service.Services
    {
        public class ArtistService : IArtistService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<ArtistService> _logger;

            public ArtistService(IUnitOfWork unitOfWork, ILogger<ArtistService> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
            {
                try
                {
                    return await _unitOfWork.Artists.GetAllTrackByArtistId(artistId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error getting tracks for artist {artistId}");
                    throw;
                }
            }

            public async Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory)
            {
                try
                {
                    return await _unitOfWork.Artists.SuggestArtists(userHistory);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error suggesting artists");
                    throw;
                }
            }

            public async Task<int> TotalArtist()
            {
                try
                {
                    return await _unitOfWork.Artists.TotalArtist();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting total artist count");
                    throw;
                }
            }

            public async Task<IEnumerable<AdminArtistDTO>> GetAdminArtists()
            {
                try
                {
                    var artists = await _unitOfWork.Artists.GetAll();

                    var adminArtists = new List<AdminArtistDTO>();

                    foreach (var artist in artists)
                    {
                        var totalSong = await _unitOfWork.Tracks.CountTrack(artist.Id);
                        var totalAlbum = await _unitOfWork.Albums.CountAlbum(artist.Id);

                        adminArtists.Add(new AdminArtistDTO
                        {
                            Id = artist.Id,
                            Name = artist.Name,
                            Image = artist.Image,
                            DOB = artist.CreateDate,
                            TotalSong = totalSong,
                            TotalAlbum = totalAlbum
                        });
                    }

                    return adminArtists;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting admin artists");
                    throw;
                }
            }

            public async Task<Artist> GetArtistByUserId(Guid userId)
            {
                try
                {
                    return await _unitOfWork.Artists.GetArtistByUserId(userId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error getting artist by user ID {userId}");
                    throw;
                }
            }
        }
    }
}
