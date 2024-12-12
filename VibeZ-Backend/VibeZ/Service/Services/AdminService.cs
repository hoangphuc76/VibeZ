using BusinessObjects;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using Repositories.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Service.Services
{
    public class AdminService(IUnitOfWork _unitOfWork, ILogger<AdminService> _logger) : IAdminService
    {


        public async Task<IEnumerable<AdminArtistDTO>> GetAdminArtists()
        {
            var artists = await _unitOfWork.Artists.GetAll();
            var adminArtists = new List<AdminArtistDTO>();

            foreach (var artist in artists)
            {
                var totalSong = await  _unitOfWork.Tracks.CountTrack(artist.Id);
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
        public async Task<TotalDataDTO> GetTotalData()
        {
            var totalAlbum = await _unitOfWork.Albums.TotalAlbum();
            var totalArtist = await _unitOfWork.Artists.TotalArtist();
            var totalPlaylist = await _unitOfWork.Playlists.TotalPlaylist();
            var totalSong = await _unitOfWork.Tracks.TotalTrack();
            var totalUser = await _unitOfWork.Users.TotalUser();

            return new TotalDataDTO
            {
                TotalAlbum = totalAlbum,
                TotalArtist = totalArtist,
                TotalPlaylist = totalPlaylist,
                TotalSong = totalSong,
                TotalUser = totalUser
            };
        }

        public async Task<AdminHomeDTO> GetAdminHome()
        {
            try
            {
                var adminArtists = await GetAdminArtists();

                var adminTotal = await GetTotalData();

                var adminHome = new AdminHomeDTO
                {
                    DataTable = adminArtists,
                    DataTotal = adminTotal
                };

                return adminHome;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Error fetching admin home data", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAdminBan()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll();
                var adminBan = users.Where(u => u.IsBanned == true).ToList();
                if (adminBan == null)
                    throw new Exception("No banned user found");
                return adminBan;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public async Task<IEnumerable<AdminApprovalDTO>> GetAdminApproval()
        {
            try
            {
                var pendingTracks = await _unitOfWork.Tracks.GetPendingTracks();
                if (pendingTracks == null)
                {
                    throw new Exception("No pending track found");
                }
                return pendingTracks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task ChangeStatusApproval(Guid trackId)
        {
            try
            {
                await _unitOfWork.Tracks.ChangeStatusApproval(trackId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
