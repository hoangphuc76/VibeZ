using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ArtistDAO : SingletonBase<ArtistDAO>
    {
        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await _context.Artists.AsNoTrackingWithIdentityResolution().ToListAsync();
        }
        public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
        {
            var artist = await _context.Artists
                .Include(la => la.Tracks)
                .FirstOrDefaultAsync(t => t.Id == artistId);
            if (artist == null)
            {
                return new List<Track>();
            }
            return artist.Tracks;
        }
        public async Task<Artist> GetArtistById(Guid artistId)
        {
            var artist = await _context.Artists.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(u => u.Id == artistId);
            if (artist == null) return null;
            return artist;
        }

        public async Task Add(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Artist artist)
        {
            _context.Attach(artist);
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }
    }

}
