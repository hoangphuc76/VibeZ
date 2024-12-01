using BusinessObjects;
using Castle.Core.Logging;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Repositories.Repository
{
    public class AdminRepository(IAlbumRepository album
            , IArtistRepository artist
            , ITrackRepository track
            , IPlaylistRepository playlist
            , IUserRepository user, ILogger<AdminRepository> logger) : IAdminRepository
    {
        public async Task<IEnumerable<AdminArtistDTO>> GetAdminArtists()
        {
            var artists = await artist.GetAllArtists();
            var adminArtists = new List<AdminArtistDTO>();

            foreach (var artist in artists)
            {
                var totalSong = await track.CountTrack(artist.Id);
                var totalAlbum = await album.CountAlbum(artist.Id);

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
            var totalAlbum = await album.TotalAlbum();
            var totalArtist = await artist.TotalArtist();
            var totalPlaylist = await playlist.TotalPlaylist();
            var totalSong = await track.TotalTrack();
            var totalUser = await user.TotalUser();

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
                logger.LogError(ex.Message, ex);
                throw new Exception("Error fetching admin home data", ex);
            }
        }
        public async Task<IEnumerable<User>> GetAdminBan()
        {
            try
            {
                var users = await user.GetAllUsers();
                var adminBan = users.Where(u => u.IsBanned == true).ToList();
                if (adminBan == null)
                    throw new Exception("No banned user found");
                return adminBan;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public async Task<IEnumerable<AdminApprovalDTO>> GetAdminApporval()
        {
            try
            {
                var pendingTracks = await track.GetPendingTracks();
                if (pendingTracks == null)
                {
                    throw new Exception("No pending track found");
                }
                return pendingTracks;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }
        public async Task ChangeStatusApporval(Guid trackId)
        {
            try
            {
                await track.ChangeStatusApproval(trackId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}