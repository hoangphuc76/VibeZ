using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TracksArtistDAO : SingletonBase<TracksArtistDAO>
    {
        public async Task<IEnumerable<Tracks_artist>> GetAllTracksArtist()
        {
            return await _context.Tracks_Artists.ToListAsync();
        }

        public async Task<Tracks_artist> GetTracksArtistById(Guid trackId, Guid artistId)
        {
            var track = await _context.Tracks_Artists.FirstOrDefaultAsync(f => f.TrackId == trackId && f.ArtistId == artistId);
            if (track == null) return null;
            return track;
        }

        public async Task Add(Tracks_artist track)
        {
            await _context.Tracks_Artists.AddAsync(track);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tracks_artist tracks_Artist)
        {
            var existingItem = await GetTracksArtistById(tracks_Artist.TrackId, tracks_Artist.ArtistId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(tracks_Artist);
            }
            else
            {
                _context.Tracks_Artists.Add(tracks_Artist);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid trackId, Guid artistId)
        {
            var track = await GetTracksArtistById(trackId, artistId);
            if (track != null)
            {
                _context.Tracks_Artists.Remove(track);
                await _context.SaveChangesAsync();
            }
        }
    }

}
