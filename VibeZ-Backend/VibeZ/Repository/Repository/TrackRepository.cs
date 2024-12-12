using BusinessObjects;
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
    public class TrackRepository : Repository<Track>, ITrackRepository

    {
        private readonly VibeZDbContext _context;
        public TrackRepository(VibeZDbContext context) : base(context) 
        {
            _context = context;
        }
 
        public async Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid albumId)
        {
            var list = await _context.Tracks
                            .Where(u => u.AlbumId == albumId)
                            .AsNoTracking()
                            .Include(t => t.Artist)
                            .Include(t => t.Album) // Tải Album cho mỗi Track
                            .OrderBy(u => u.CreateDate)
                            .ToListAsync();

            return list;
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
                var track = await GetById(trackId);
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

        public async Task UpdateListener(Track track)
        {
            // Kiểm tra xem thực thể có đang được theo dõi hay không
            var existingTrack = await _context.Tracks.FindAsync(track.TrackId);

            if (existingTrack != null)
            {
                // Nếu thực thể đã tồn tại trong DbContext, cập nhật giá trị Listener
                existingTrack.Listener += 1;
                existingTrack.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                // Nếu không, đính kèm và cập nhật trực tiếp
                _context.Attach(track);
                track.Listener += 1;
                _context.Entry(track).Property(x => x.Listener).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds)
        {
            var list = await _context.Tracks
               .Where(track => trackIds.Contains(track.TrackId))
               .AsNoTracking()
               .Include(t => t.Artist)
               .Include(t => t.Album) // Tải Album cho mỗi Track
               .ToListAsync();

            return list;
        }

        private async Task<IEnumerable<Track>> RecommendSongsBasedOnRecentTracks(List<Guid> recentlyPlayedIds)
        {

            // Lấy các bài hát đã nghe gần đây và tải thông tin liên quan (Artist, Album)
            var recentlyPlayedTracks = await _context.Tracks
                .Where(track => recentlyPlayedIds.Contains(track.TrackId))
                .AsNoTracking()
                .Include(t => t.Artist) // Tải thông tin Artist cho mỗi Track đã nghe gần đây
                .Include(t => t.Album)  // Tải thông tin Album cho mỗi Track đã nghe gần đây
                .ToListAsync();

            if (!recentlyPlayedTracks.Any())
                return Enumerable.Empty<Track>();

            // Lấy thể loại và nghệ sĩ của các bài hát đã nghe
            var genres = recentlyPlayedTracks.Select(t => t.Genre).Distinct().ToList();
            var artists = recentlyPlayedTracks.Select(t => t.ArtistId).Distinct().ToList();

            // Gợi ý các bài hát khác thuộc cùng thể loại hoặc nghệ sĩ
            var recommendedTracks = await _context.Tracks
                .Where(track => genres.Contains(track.Genre) || artists.Contains(track.ArtistId))
                .Where(track => !recentlyPlayedIds.Contains(track.TrackId))
                .AsNoTracking()// Loại bỏ bài hát đã nghe
                .Include(t => t.Artist) // Tải thông tin Artist cho các bài hát gợi ý
                .Include(t => t.Album)  // Tải thông tin Album cho các bài hát gợi ý
                .ToListAsync();

            return recommendedTracks;
        }
        private async Task<IEnumerable<Track>> RecommendPopularSongs(int topN = 10)
        {

            // Lấy các bài hát phổ biến nhất dựa trên số lượt nghe (listener count)
            var popularTracks = await _context.Tracks
                .OrderByDescending(track => track.Listener) // Sắp xếp theo số lượt nghe
                .Take(topN)
                .AsNoTracking()// Lấy top N bài hát phổ biến nhất
                .Include(t => t.Artist) // Tải thông tin Artist cho mỗi Track
                .Include(t => t.Album)  // Tải thông tin Album cho mỗi Track
                .ToListAsync();

            return popularTracks;
        }
        private async Task<IEnumerable<Track>> RecommendRandomSongs(int topN = 10)
        {

            var random = new Random();

            // Lấy ngẫu nhiên top N bài hát từ cơ sở dữ liệu
            var randomTracks = await _context.Tracks
                .OrderBy(x => random.Next())  // Random gợi ý
                .Take(topN)
                .AsNoTracking()
                .Include(t => t.Artist) // Tải thông tin Artist cho mỗi Track
                .Include(t => t.Album)  // Tải thông tin Album cho mỗi Track
                .ToListAsync();

            return randomTracks;
        }


      
        public async Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10)
        {
            // Lấy bài hát vừa được click và tải thông tin liên quan (Artist, Album)
            var clickedTrack = await _context.Tracks
                .AsNoTracking()
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .FirstOrDefaultAsync(t => t.TrackId == clickedTrackId);

            if (clickedTrack == null)
                return Enumerable.Empty<Track>();

            // Gợi ý dựa trên các bài hát đã nghe gần đây
            var contentBasedRecommendations = await RecommendSongsBasedOnRecentTracks(recentlyPlayedIds);

            // Nếu không có bài hát gợi ý dựa trên nội dung, gợi ý theo độ phổ biến
            if (!contentBasedRecommendations.Any())
            {
                contentBasedRecommendations = await RecommendPopularSongs(topN);
            }

            // Nếu vẫn không có, gợi ý bài hát ngẫu nhiên
            if (!contentBasedRecommendations.Any())
            {
                contentBasedRecommendations = await RecommendRandomSongs(topN);
            }

            // Đặt bài hát vừa click lên đầu danh sách
            var recommendationQueue = new List<Track> { clickedTrack };
            recommendationQueue.AddRange(contentBasedRecommendations.Where(t => t.TrackId != clickedTrackId)); // Loại bỏ bài đã click trong gợi ý

            return recommendationQueue;
        }


    }
}
