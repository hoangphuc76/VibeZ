using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PlaylistDAO : SingletonBase<PlaylistDAO>
    {
     
        // Lấy tất cả playlists theo UserId, không theo dõi thực thể
        public async Task<IEnumerable<Playlist>> GetAllPlaylistsByUserId(Guid id)
        {
            return await _context.Playlists
                                 .Where(x => x.UserId == id)
                                 .AsNoTracking()  // Không theo dõi thực thể
                                 .ToListAsync();
        }

        // Lấy tất cả playlists mà không theo dõi
        public async Task<IEnumerable<Playlist>> GetAllPlaylist()
        {
            return await _context.Playlists
                                 .AsNoTracking()  // Không theo dõi thực thể
                                 .ToListAsync();
        }

        // Lấy tất cả các Track thuộc Playlist
        public async Task<IEnumerable<Track>> GetTracksByPlaylistId(Guid playlistId)
        {
            var playlist = await _context.Playlists
                     .AsNoTracking()
                     .AsSplitQuery() // Chia truy vấn thành nhiều phần
                     .Include(p => p.TrackPlayLists)
                        .ThenInclude(tp => tp.Track)
                        .ThenInclude(t => t.Album)
                     .Include(p => p.TrackPlayLists)
                        .ThenInclude(tp => tp.Track)
                        .ThenInclude(t => t.Artist)
                     .FirstOrDefaultAsync(l => l.PlaylistId == playlistId);

            if (playlist == null)
                return new List<Track>();

            return playlist.TrackPlayLists.Select(la => la.Track);
        }


        // Lấy một Playlist theo Id mà không theo dõi
        public async Task<Playlist> GetPlaylistById(Guid playlistId)
        {
            return await _context.Playlists
                                 .AsNoTracking()  // Không theo dõi thực thể
                                 .FirstOrDefaultAsync(f => f.PlaylistId == playlistId);
        }

        // Thêm mới một Playlist
        public async Task Add(Playlist playlist)
        {
            // Kiểm tra nếu playlist đã có trong bộ nhớ theo dõi của DbContext
            var existingEntity = _context.Playlists.Local.FirstOrDefault(p => p.PlaylistId == playlist.PlaylistId);

            // Chỉ thêm nếu playlist chưa tồn tại trong bộ nhớ theo dõi
            if (existingEntity == null)
            {
                await _context.Playlists.AddAsync(playlist);
                await _context.SaveChangesAsync();
            }
        }

        // Cập nhật Playlist mà không cần kiểm tra theo dõi
        public async Task Update(Playlist playlist)
        {
            var existingEntity = await _context.Playlists.FindAsync(playlist.PlaylistId);

            if (existingEntity != null)
            {
                // Nếu đã có thực thể, cập nhật giá trị hiện tại
                _context.Entry(existingEntity).CurrentValues.SetValues(playlist);
            }
            else
            {
                // Nếu chưa tồn tại, thêm mới thực thể
                _context.Playlists.Update(playlist);
            }

            await _context.SaveChangesAsync();
        }


        // Xóa Playlist mà không cần kiểm tra theo dõi
        public async Task Delete(Playlist playlist)
        {
            var existingEntity = await _context.Playlists.FindAsync(playlist.PlaylistId);

            // Chỉ xóa nếu thực thể tồn tại
            if (existingEntity != null)
            {
                _context.Playlists.Remove(existingEntity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
