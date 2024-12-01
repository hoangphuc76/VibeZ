using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LibraryDAO : SingletonBase<LibraryDAO>
    {

        public async Task<IEnumerable<Library>> GetAllLibraries()
        {
            return await _context.Libraries.AsNoTracking().ToListAsync();
        }

        public async Task<Library> GetLibraryById(Guid userId) // Get library by user ID
        {
            // Sử dụng Include để tải các liên kết nếu cần
            var library = await _context.Libraries
                                        .Include(l => l.Library_Albums)
                                        .Include(l => l.Library_Playlists)
                                        .Include(l => l.Library_Artist)
                                        .AsNoTracking() // Không cần tracking nếu không cập nhật
                                        .FirstOrDefaultAsync(l => l.UserId == userId);

            return library;
        }

        public async Task Add(Library library)
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByLibraryId(Guid libraryId)
        {
            // Lấy Library cùng với liên kết Album trong một truy vấn
            var library = await _context.Libraries
                                        .Include(l => l.Library_Albums)
                                        .ThenInclude(la => la.Album) // Sử dụng ThenInclude để tải album
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Albums.Select(la => la.Album) ?? new List<Album>();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsByLibraryId(Guid libraryId)
        {
            // Lấy Library cùng với liên kết Playlist trong một truy vấn
            var library = await _context.Libraries
                                        .Include(l => l.Library_Playlists)
                                        .ThenInclude(lp => lp.Playlist)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Playlists.Select(lp => lp.Playlist) ?? new List<Playlist>();
        }

        public async Task<IEnumerable<Artist>> GetArtistByLibraryId(Guid libraryId)
        {
            // Lấy Library cùng với liên kết Artist trong một truy vấn
            var library = await _context.Libraries
                                        .Include(l => l.Library_Artist)
                                        .ThenInclude(la => la.Artist)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Artist.Select(la => la.Artist) ?? new List<Artist>();
        }

        public async Task Update(Library library)
        {
            var existingItem = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == library.Id);

            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(library);
            }
            else
            {
                await _context.Libraries.AddAsync(library);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid libraryId)
        {
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == libraryId);
            if (library != null)
            {
                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
        }
    }
}
