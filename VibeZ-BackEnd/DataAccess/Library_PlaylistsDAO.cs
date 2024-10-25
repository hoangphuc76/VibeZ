using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Library_PlaylistsDAO : SingletonBase<Library_PlaylistsDAO>
    {
        public async Task<IEnumerable<Library_Playlist>> GetAllLibrariePlaylists()
        {
            return await _context.Library_Playlists.AsNoTracking().ToListAsync();
        }

        public async Task<Library_Playlist> GetLibraryPlaylistById(Guid libraryId, Guid playlistId)
        {
            // Sử dụng AsNoTracking để tránh việc tracking các thực thể chỉ dùng để đọc
            var lbp = await _context.Library_Playlists
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(f => f.LibraryId == libraryId && f.PlaylistId == playlistId);
            return lbp;
        }

        public async Task Add(Library_Playlist library_Playlists)
        {
            // Kiểm tra xem thực thể đã tồn tại trong DbContext hay chưa
            var trackedEntity = _context.Library_Playlists.Local
                                 .FirstOrDefault(lp => lp.LibraryId == library_Playlists.LibraryId &&
                                                       lp.PlaylistId == library_Playlists.PlaylistId);

            if (trackedEntity == null)
            {
                var existingEntity = await _context.Library_Playlists
                                                   .FirstOrDefaultAsync(lp => lp.LibraryId == library_Playlists.LibraryId &&
                                                                              lp.PlaylistId == library_Playlists.PlaylistId);

                // Kiểm tra nếu thực thể đã tồn tại, không thêm mới nữa
                if (existingEntity == null)
                {
                    await _context.Library_Playlists.AddAsync(library_Playlists);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new InvalidOperationException("An instance with the same key is already being tracked.");
            }
        }

        public async Task Update(Library_Playlist library_Playlists)
        {
            var existingItem = await GetLibraryPlaylistById(library_Playlists.LibraryId, library_Playlists.PlaylistId);

            if (existingItem != null)
            {
                // Nếu thực thể đang được theo dõi, detach thực thể cũ để tránh lỗi
                var trackedEntity = _context.Library_Playlists.Local
                                      .FirstOrDefault(lp => lp.LibraryId == library_Playlists.LibraryId &&
                                                            lp.PlaylistId == library_Playlists.PlaylistId);

                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }

                // Cập nhật thực thể
                _context.Entry(existingItem).CurrentValues.SetValues(library_Playlists);
            }
            else
            {
                // Nếu không tồn tại, thêm mới thực thể
                await Add(library_Playlists);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid libraryId, Guid playlistId)
        {
            var library_Playlists = await GetLibraryPlaylistById(libraryId, playlistId);
            if (library_Playlists != null)
            {
                _context.Library_Playlists.Remove(library_Playlists);
                await _context.SaveChangesAsync();
            }
        }
    }
}
