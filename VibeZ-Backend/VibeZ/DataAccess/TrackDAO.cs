using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TrackDAO : SingletonBase<TrackDAO>
    {
        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            using var context = new VibeZDbContext();

            // Sử dụng Include để tải thông tin Artist và Album cho mỗi Track
            var list = await _context.Tracks
                .AsNoTracking()
                .Include(x => x.Album) // Tải Album cho mỗi Track
                .Include(t => t.Artist) // Tải Artist cho mỗi Track
                .ToListAsync();

            return list;
        }
        public async Task<int> CountTrack(Guid artistId)
        {
            using var context = new VibeZDbContext();

            return await _context.Albums
                .Where(album => album.ArtistId == artistId)
                .SelectMany(album => _context.Tracks.Where(track => track.AlbumId == album.Id))
                .CountAsync();
        }

        public async Task<Track> GetTrackById(Guid trackId)
        {
            using var context = new VibeZDbContext();

            // Sử dụng Include để tải thông tin Artist và Album cho Track theo Id
            var track = await _context.Tracks
                .AsNoTracking()
                .Include(t => t.Artist)
                .Include(t => t.Album) // Tải Album cho Track
                .FirstOrDefaultAsync(u => u.TrackId == trackId);

            return track;
        }

        public async Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid albumId)
        {
            using var context = new VibeZDbContext();

            // Sử dụng Include để tải thông tin Artist và Album cho mỗi Track
            var list = await _context.Tracks
                .Where(u => u.AlbumId == albumId)
                .AsNoTracking()
                .Include(t => t.Artist)
                .Include(t => t.Album) // Tải Album cho mỗi Track
                .OrderBy(u => u.CreateDate)
                .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds)
        {
            using var context = new VibeZDbContext();

            // Sử dụng Include để tải thông tin Artist và Album cho mỗi Track theo danh sách Id
            var list = await _context.Tracks
                .Where(track => trackIds.Contains(track.TrackId))
                .AsNoTracking()
                .Include(t => t.Artist)
                .Include(t => t.Album) // Tải Album cho mỗi Track
                .ToListAsync();

            return list;
        }

        public async Task Add(Track track)
        {
            using var context = new VibeZDbContext();

            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateListener(Track track)
        {
            using var context = new VibeZDbContext();

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

        public async Task Update(Track track)
        {
            using var context = new VibeZDbContext();
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Track track)
        {
            using var context = new VibeZDbContext();

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
        }

        // Gợi ý dựa trên thể loại và nghệ sĩ của các bài hát đã nghe gần đây
        public async Task<IEnumerable<Track>> RecommendSongsBasedOnRecentTracks(List<Guid> recentlyPlayedIds)
        {
            using var context = new VibeZDbContext();

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

        // Gợi ý bài hát dựa trên số lượng lượt nghe (Popular-based Recommendations)
        public async Task<IEnumerable<Track>> RecommendPopularSongs(int topN = 10)
        {
            using var context = new VibeZDbContext();

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

        // Gợi ý ngẫu nhiên bài hát (Random-based Recommendations)
        public async Task<IEnumerable<Track>> RecommendRandomSongs(int topN = 10)
        {
            using var context = new VibeZDbContext();

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

        // Kết hợp tất cả các gợi ý và đưa bài nhạc vừa click lên đầu danh sách
        public async Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10)
        {
            using var context = new VibeZDbContext();

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
