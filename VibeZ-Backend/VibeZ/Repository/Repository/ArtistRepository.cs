using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VibeZDTO;

namespace Repositories.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly VibeZDbContext _context;
        public ArtistRepository()
        {
            _context = new VibeZDbContext();
        }
        public async Task<int> TotalArtist()
        {
            return await Task.FromResult(_context.Artists.AsNoTrackingWithIdentityResolution().Count());
        }
        public async Task<IEnumerable<AdminArtistDTO>> GetAdminArtists()
        {
            var artists = await _context.Artists.AsNoTrackingWithIdentityResolution().ToListAsync();

            var adminArtists = new List<AdminArtistDTO>();

            foreach (var artist in artists)
            {
                var totalSong = await TrackDAO.Instance.CountTrack(artist.Id);
                var totalAlbum = await AlbumDAO.Instance.CountAlbum(artist.Id);

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
        public async Task<Artist> GetArtistByUserId(Guid userId)
        {
            return await _context.Artists.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await ArtistDAO.Instance.GetAllArtists();
        }

        public async Task<Artist> GetArtistById(Guid artistId)
        {
            return await ArtistDAO.Instance.GetArtistById(artistId);
        }

        public async Task AddArtist(Artist artist)
        {
            await ArtistDAO.Instance.Add(artist);
        }

        public async Task UpdateArtist(Artist artist)
        {
            await ArtistDAO.Instance.Update(artist);
        }

        public async Task DeleteArtist(Artist artist)
        {
            await ArtistDAO.Instance.Delete(artist.Id);
        }
        public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
        {
           return await ArtistDAO.Instance.GetAllTrackByArtistId(artistId);
        }
        public async Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory)
        {
            return await ArtistDAO.Instance.SuggestArtists(userHistory);
        }

    }
}
