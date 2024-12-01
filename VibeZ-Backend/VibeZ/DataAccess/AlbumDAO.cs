using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AlbumDAO : SingletonBase<AlbumDAO>
    {

        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            using var context = new VibeZDbContext();
            return await _context.Albums.AsNoTrackingWithIdentityResolution()
                .Include(x => x.Artist)
                .ToListAsync();
        }
        public async Task<int> CountAlbum(Guid artistId)
        {
            using var context = new VibeZDbContext();
            return await _context.Albums.CountAsync(album => album.ArtistId == artistId);
        }
        public async Task<int> TotalAlbum()
        {
            using var context = new VibeZDbContext();

            return await Task.FromResult(_context.Albums.Count());
        }
        public async Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId)
        {
            using var context = new VibeZDbContext();

            return await _context.Albums.Where(x => x.ArtistId == artistId).AsNoTrackingWithIdentityResolution().Include(x => x.Artist).ToListAsync();
        }

        public async Task<Album> GetAlbumById(Guid albumId)
        {
            using var context = new VibeZDbContext();

            var album = await _context.Albums.AsNoTrackingWithIdentityResolution().Include(x => x.Artist).FirstOrDefaultAsync(u => u.Id == albumId); ;
            return album;
        }

        public async Task Add(Album album)
        {
            using var context = new VibeZDbContext();

            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Album album)
        {
            using var context = new VibeZDbContext();

            _context.Attach(album);
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Album album)
        {
            using var context = new VibeZDbContext();

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }
    }

}
