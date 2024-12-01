using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ArtistDAO : SingletonBase<ArtistDAO>
    {

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await _context.Artists.AsNoTracking().ToListAsync();
        }

        // Lấy tất cả track liên quan đến nghệ sĩ với artistId
        public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
        {
            var artist = await _context.Artists
                                       .Include(a => a.Tracks) // Sử dụng Include để nạp track liên quan
                                       .AsNoTracking()         // Không tracking để tối ưu hiệu suất
                                       .FirstOrDefaultAsync(a => a.Id == artistId);

            return artist?.Tracks ?? new List<Track>(); // Trả về danh sách rỗng nếu không tìm thấy
        }

        // Lấy nghệ sĩ theo Id mà không cần tracking
        public async Task<Artist> GetArtistById(Guid artistId)
        {
            var artist = await _context.Artists
                                       .AsNoTracking()  // Không cần tracking nếu chỉ đọc dữ liệu
                                       .FirstOrDefaultAsync(a => a.Id == artistId);

            return artist;
        }

        // Thêm nghệ sĩ mới vào cơ sở dữ liệu
        public async Task Add(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin nghệ sĩ
        public async Task Update(Artist artist)
        {
            var existingArtist = await _context.Artists.FindAsync(artist.Id);
            if (existingArtist != null)
            {
                _context.Entry(existingArtist).CurrentValues.SetValues(artist); // Cập nhật giá trị
                await _context.SaveChangesAsync();
            }
        }

        // Xóa nghệ sĩ
        public async Task Delete(Guid artistId)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == artistId);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Artist>> GetUnheardArtists(List<Guid> userHistory)
        {
            var artist = await _context.Artists.Where(a => !userHistory.Contains(a.Id)).AsNoTracking().ToListAsync();
            return artist;
        }
        public async Task<IDictionary<Guid, int>> ArtistPopularity()
        {
            var artist = await _context.Artists
                                      .Include(a => a.Tracks) // Sử dụng Include để nạp track liên quan
                                      .AsNoTracking()
                                      .ToListAsync();

            if (artist == null || !artist.Any())
            {
                return new Dictionary<Guid, int>();
            }
            var artistPopularity = artist.ToDictionary(
                                     a => a.Id, // Key: ArtistId (kiểu Guid)
                                    a => a.Tracks.Sum(track => track.Listener)); // Value: Tổng số listener của tất cả các track
            return artistPopularity;
        }
        public async Task<IEnumerable<Artist>> SuggestArtists(List<Guid> userHistory)
        {
            var unheardArtist = await GetUnheardArtists(userHistory);
            var artistPopularity = await ArtistPopularity();
            var sortedArtists = unheardArtist.OrderByDescending(a => artistPopularity.ContainsKey(a.Id) ? artistPopularity[a.Id] : 0);

            return sortedArtists.Take(10).ToList();

        }
    }
}
