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
            return await _context.Albums.AsNoTrackingWithIdentityResolution().ToListAsync();
        }
        public async Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId)
        {
            return await _context.Albums.Where(x => x.ArtistId == artistId).AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<Album> GetAlbumById(Guid albumId)
        {
            var album = await _context.Albums.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(u => u.Id == albumId); ;
            return album;
        }

        public async Task Add(Album album)
        {   
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Album album)
        {
            _context.Attach(album);
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Album album)
        {
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }
    }

}
