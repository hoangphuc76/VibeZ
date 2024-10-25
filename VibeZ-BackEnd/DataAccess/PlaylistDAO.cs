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
                                         .AsNoTracking()  // Không theo dõi playlist
                                         .FirstOrDefaultAsync(l => l.PlaylistId == playlistId);

            if (playlist == null)
                return new List<Track>();  // Trả về danh sách rỗng nếu không tìm thấy Playlist

            // Explicit loading: Nạp các Track liên quan
            await _context.Entry(playlist)
                          .Collection(l => l.TrackPlayLists)
                          .Query()
                          .Include(la => la.Track)
                          .ThenInclude(la => la.Album)
                          .Include(la => la.Track)
                          .ThenInclude(la => la.Artist)
                          .LoadAsync();

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
            var existingEntity = _context.Playlists.Local.FirstOrDefault(p => p.PlaylistId == playlist.PlaylistId);
            if (existingEntity == null)  // Chỉ thêm nếu playlist chưa tồn tại
            {
                await _context.Playlists.AddAsync(playlist);
                await _context.SaveChangesAsync();
            }
        }
        // Cập nhật Playlist mà không cần kiểm tra theo dõi
        public async Task Update(Playlist playlist)
        {
            var existingEntity = _context.Playlists.Local.FirstOrDefault(p => p.PlaylistId == playlist.PlaylistId);
            if (existingEntity != null)
            {
                // Nếu thực thể đã tồn tại, bạn có thể cập nhật nó
                _context.Entry(existingEntity).CurrentValues.SetValues(playlist);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Nếu chưa tồn tại, có thể thêm thực thể mới
                _context.Playlists.Update(playlist);
                await _context.SaveChangesAsync();
            }
        }

        // Xóa Playlist mà không cần kiểm tra theo dõi
        public async Task Delete(Playlist playlist)
        {
            var existingEntity = _context.Playlists.Local.FirstOrDefault(p => p.PlaylistId == playlist.PlaylistId);
            if (existingEntity != null)  // Chỉ xóa nếu playlist tồn tại
            {
                _context.Playlists.Remove(existingEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
