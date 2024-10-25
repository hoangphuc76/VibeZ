using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TrackDAO : SingletonBase<TrackDAO>
    {
        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            var list= await _context.Tracks.AsNoTracking().ToListAsync();
            if (list == null || !list.Any()) return null;

            // Thực hiện explicit loading cho mỗi Track trong danh sách
            foreach (var track in list)
            {
                await _context.Entry(track)
                    .Reference(t => t.Artist)  // Tải thông tin Artist của mỗi Track
                    .LoadAsync();

                // Nếu có các dữ liệu liên quan khác (ví dụ Album), bạn cũng có thể tải tương tự
                // await _context.Entry(track)
                //     .Reference(t => t.Album)
                //     .LoadAsync();
            }
            return list;
        }
        

        public async Task<Track> GetTrackById(Guid trackId)
        {
            var track = await _context.Tracks.
                AsNoTracking().
                FirstOrDefaultAsync(u => u.TrackId == trackId);
            if (track != null)
            {
                await _context.Entry(track)
           .Reference(t => t.Artist) // Chỉ định Reference (1-1 hoặc N-1)
           .LoadAsync();
            }
            return track;
        }
        public async Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid albumId)
        {
            // Lấy danh sách các Track theo AlbumId
            var list = await _context.Tracks
                .Where(u => u.AlbumId == albumId)
                .AsNoTracking()
                .OrderBy(u => u.CreateDate)
                .ToListAsync();

            // Nếu danh sách rỗng, trả về null
            if (list == null || !list.Any()) return null;

            // Thực hiện explicit loading cho mỗi Track trong danh sách
            foreach (var track in list)
            {
                await _context.Entry(track)
                    .Reference(t => t.Artist)  // Tải thông tin Artist của mỗi Track
                    .LoadAsync();

                // Nếu có các dữ liệu liên quan khác (ví dụ Album), bạn cũng có thể tải tương tự
                // await _context.Entry(track)
                //     .Reference(t => t.Album)
                //     .LoadAsync();
            }

            return list;
        }
        public async Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds)
        {
            var list= await _context.Tracks
                .Where(track => trackIds.Contains(track.TrackId))
                .ToListAsync();  // Truy vấn một lần duy nhất với tất cả trackId

            if (list == null || !list.Any()) return null;

            // Thực hiện explicit loading cho mỗi Track trong danh sách
            foreach (var track in list)
            {
                await _context.Entry(track)
                    .Reference(t => t.Artist)  // Tải thông tin Artist của mỗi Track
                    .LoadAsync();

                // Nếu có các dữ liệu liên quan khác (ví dụ Album), bạn cũng có thể tải tương tự
                // await _context.Entry(track)
                //     .Reference(t => t.Album)
                //     .LoadAsync();
            }
            return list;

        }
        public async Task Add(Track track)
        {
            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateListener(Track track)
        {
            // Kiểm tra xem thực thể có đang được theo dõi hay không
            var existingTrack = await _context.Tracks.FindAsync(track.TrackId);

            if (existingTrack != null)
            {
                // Nếu thực thể đã tồn tại trong DbContext, cập nhật giá trị Listener
                existingTrack.Listener += 1;
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
            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Track track)
        {

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();

        }
        // Gợi ý dựa trên thể loại và nghệ sĩ của các bài hát đã nghe gần đây
        public async Task<IEnumerable<Track>> RecommendSongsBasedOnRecentTracks(List<Guid> recentlyPlayedIds)
        {
            // Lấy các bài hát đã nghe gần đây
            var recentlyPlayedTracks = await _context.Tracks
                .Where(track => recentlyPlayedIds.Contains(track.TrackId))
                .ToListAsync();

            if (!recentlyPlayedTracks.Any())
                return Enumerable.Empty<Track>();

            // Lấy thể loại và nghệ sĩ của các bài hát đã nghe
            var genres = recentlyPlayedTracks.Select(t => t.Genre).Distinct().ToList();
            var artists = recentlyPlayedTracks.Select(t => t.ArtistId).Distinct().ToList();

            // Gợi ý các bài hát khác thuộc cùng thể loại hoặc nghệ sĩ
            var recommendedTracks = await _context.Tracks
                .Where(track => genres.Contains(track.Genre) || artists.Contains(track.ArtistId))
                .Where(track => !recentlyPlayedIds.Contains(track.TrackId)) // Loại bỏ bài hát đã nghe
                .ToListAsync();

            return recommendedTracks;
        }

        // Gợi ý bài hát dựa trên số lượng lượt nghe (Popular-based Recommendations)
        public async Task<IEnumerable<Track>> RecommendPopularSongs(int topN = 10)
        {
            // Lấy các bài hát phổ biến nhất dựa trên số lượt nghe (listener count)
            var popularTracks = await _context.Tracks
                .OrderByDescending(track => track.Listener) // Sắp xếp theo số lượt nghe
                .Take(topN)  // Lấy top N bài hát phổ biến nhất
                .ToListAsync();

            return popularTracks;
        }

        // Gợi ý ngẫu nhiên bài hát (Random-based Recommendations)
        public async Task<IEnumerable<Track>> RecommendRandomSongs(int topN = 10)
        {
            var random = new Random();

            // Lấy ngẫu nhiên top N bài hát từ cơ sở dữ liệu
            var randomTracks = await _context.Tracks
                .OrderBy(x => random.Next())  // Random gợi ý
                .Take(topN)
                .ToListAsync();

            return randomTracks;
        }

        // Kết hợp tất cả các gợi ý và đưa bài nhạc vừa click lên đầu danh sách
        public async Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10)
        {
            // Lấy bài hát vừa được click
            var clickedTrack = await _context.Tracks
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
