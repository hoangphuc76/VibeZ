using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Repositories.Repository
{
    public class TrackRepository : ITrackRepository

    {
        private readonly VibeZDbContext _context;
        public TrackRepository()
        {
            _context = new VibeZDbContext();
        }
        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            return await TrackDAO.Instance.GetAllTracks();
        }
        public async Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid id)
        {
            return await TrackDAO.Instance.GetAllTrackByAlbumId(id);
        }
        public async Task<int> TotalTrack()
        {
            return await Task.FromResult(_context.Tracks.Count());
        }
        public async Task<int> CountTrack(Guid artistId)
        {
            return await _context.Albums
                .Where(album => album.ArtistId == artistId)
                .SelectMany(album => _context.Tracks.Where(track => track.AlbumId == album.Id))
                .CountAsync();
        }
        //public async Task<IEnumerable<Track>> GetAllTrackByArtistId()a
        public async Task<int> CountTotalListenerByArtist(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            var result = await _context.Tracks.Where(x => x.ArtistId == artistId && x.PendingApproval == false)
                                        .AsNoTracking()
                                        .Include(x => x.TrackListeners).ToListAsync();
            var sum = result.SelectMany(x => x.TrackListeners).Where(x => x.Date >= startDate && x.Date <= endDate)
                            .Sum(x => x.Listener);

            return sum;
        }
        public async Task<IEnumerable<Track>> GetTop10Songs(Guid artistId, DateOnly startDate, DateOnly endDate)
        {
            var result = await _context.Tracks
                                             .Where(x => x.ArtistId == artistId && x.PendingApproval == false)
                                             .AsNoTracking()
                                             .Include(x => x.TrackListeners)
                                             .Select(track => new
                                             {
                                                 Track = track,
                                                 ListenerCount = track.TrackListeners
                                                                     .Where(tl => tl.Date >= startDate && tl.Date <= endDate)
                                                                     .Sum(x => x.Listener)
                                             })
                                            .OrderByDescending(x => x.ListenerCount)
                                            .Take(10)
                                            .Select(x => x.Track) // Lấy lại đối tượng Track sau khi sắp xếp
                                            .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<AdminApprovalDTO>> GetPendingTracks()
        {
            try
            {
                var pendingTracks = await _context.Tracks
                    .AsNoTracking()
                    .Where(track => track.PendingApproval)
                    .Select(track => new AdminApprovalDTO
                    {
                        TrackId = track.TrackId,
                        Image = track.Artist.Image,
                        SongName = track.Name,
                        WriterName = track.Artist.Name,
                        DateCreated = track.CreateDate,
                        AlbumName = track.Album.Name,
                        Path = track.Path,
                    })
                    .ToListAsync();

                if (!pendingTracks.Any())
                    throw new Exception("No pending tracks found");

                return pendingTracks;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching pending tracks with artist name", ex);
            }
        }
        public async Task ChangeStatusApproval(Guid trackId)
        {
            try
            {
                var track = await GetTrackById(trackId);
                if (track is null)
                {
                    throw new Exception("Track not found");
                }

                track.PendingApproval = false;

                _context.Attach(track);
                _context.Entry(track).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing approval status", ex);
            }
        }
        public async Task<Track> GetTrackById(Guid trackId)
        {
            return await TrackDAO.Instance.GetTrackById(trackId);
        }
        public async Task UpdateListener(Track track)
        {
            await TrackDAO.Instance.UpdateListener(track);
        }

        public async Task AddTrack(Track track)
        {
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrack(Track track)
        {
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrack(Track track)
        {
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds) => await TrackDAO.Instance.GetTrackByIds(trackIds);

        public async Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10)
        {
            return await TrackDAO.Instance.GetSongRecommendations(recentlyPlayedIds, clickedTrackId, topN);
        }


    }
}
