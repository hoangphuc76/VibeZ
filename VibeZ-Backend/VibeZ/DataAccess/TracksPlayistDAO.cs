using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TracksPlaylistDAO : SingletonBase<TracksPlaylistDAO>
    {
        public async Task<IEnumerable<Track_Playlist>> GetAllTracksPlaylist()
        {
            using var context = new VibeZDbContext();
            return await context.TrackPlayLists.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<Track_Playlist> GetTracksPlaylistById(Guid trackId, Guid playlistId)
        {
            using var context = new VibeZDbContext();
            return await context.TrackPlayLists
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(f => f.TrackId == trackId && f.PlaylistId == playlistId);
        }

        public async Task Add(Track_Playlist trackPlayList)
        {
            using var context = new VibeZDbContext();
            if (await GetTracksPlaylistById(trackPlayList.TrackId, trackPlayList.PlaylistId) == null)
            {
                await context.TrackPlayLists.AddAsync(trackPlayList);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entry already exists.");
            }
        }

        public async Task Update(Track_Playlist trackPlayList)
        {
            using var context = new VibeZDbContext();
            var existingTrack = await GetTracksPlaylistById(trackPlayList.TrackId, trackPlayList.PlaylistId);
            if (existingTrack != null)
            {
                context.TrackPlayLists.Update(trackPlayList);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Failed to update: Entry does not exist.");
            }
        }

        public async Task Delete(Track_Playlist trackPlayList)
        {
            using var context = new VibeZDbContext();
            var track = await GetTracksPlaylistById(trackPlayList.TrackId, trackPlayList.PlaylistId);
            if (track != null)
            {
                context.TrackPlayLists.Remove(track);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Failed to delete: Entry does not exist.");
            }
        }
    }
}
