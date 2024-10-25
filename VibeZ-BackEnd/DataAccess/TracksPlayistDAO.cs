using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TracksPlaylistDAO : SingletonBase<TracksPlaylistDAO>
    {
        public async Task<IEnumerable<Track_Playlist>> GetAllTracksPlaylist()
        {
            return await _context.TrackPlayLists.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<Track_Playlist> GetTracksPlaylistById(Guid trackId, Guid playlistId)
        {
            var track = await _context.TrackPlayLists.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(f => f.TrackId == trackId && f.PlaylistId == playlistId);
            if (track == null) return null;
            return track;
        }

        public async Task Add(Track_Playlist trackPlayList)
        {
            await _context.TrackPlayLists.AddAsync(trackPlayList);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Track_Playlist trackPlayList)
        {
            _context.Attach(trackPlayList);
            _context.Entry(trackPlayList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Track_Playlist trackPlayList)
        {
                _context.TrackPlayLists.Remove(trackPlayList);
                await _context.SaveChangesAsync();
        }
    }

}
