using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LibraryDAO : SingletonBase<LibraryDAO>
    {
        public async Task<IEnumerable<Library>> GetAllLibraries()
        {
            return await _context.Libraries.ToListAsync();
        }
        public async Task<Library> GetLibraryById(Guid userId) //Get library by user id
        {
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.UserId == userId);
            if (library == null) return null;
            return library;
        }

        public async Task Add(Library library)
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Album>> GetAlbumsByLibraryId(Guid libraryId)
        {
            // Tìm Library với libraryId
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == libraryId);

            if (library == null)
                return new List<Album>(); // Trả về danh sách rỗng nếu không tìm thấy Library

            // Explicit loading: Nạp các album liên quan qua bảng Library_Album
            await _context.Entry(library)
                          .Collection(l => l.Library_Albums)
                          .Query()
                          .Include(la => la.Album) // Nạp các album từ LibraryAlbum
                          .LoadAsync();

            // Trả về danh sách album
            return library.Library_Albums.Select(la => la.Album);  // Không cần ToList() nếu trả về IEnumerable
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsByLibraryId(Guid libraryId)
        {
            // Tìm Library với libraryId
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == libraryId);

            if (library == null)
                return new List<Playlist>(); // Trả về danh sách rỗng nếu không tìm thấy Library

            // Explicit loading: Nạp các playlist liên quan qua bảng Library_Playlist
            await _context.Entry(library)
                          .Collection(l => l.Library_Playlists)
                          .Query()
                          .Include(lp => lp.Playlist) // Nạp các playlist từ Library_Playlist
                          .LoadAsync();

            // Trả về danh sách playlist
            return library.Library_Playlists.Select(lp => lp.Playlist);  // Không cần ToList() nếu trả về IEnumerable
        }

        public async Task<IEnumerable<Artist>> GetArtistByLibraryId(Guid libraryId)
        {
            // Tìm Library với libraryId
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == libraryId);

            if (library == null)
                return new List<Artist>(); // Trả về danh sách rỗng nếu không tìm thấy Library

            // Explicit loading: Nạp các playlist liên quan qua bảng Library_Playlist
            await _context.Entry(library)
                          .Collection(l => l.Library_Artist)
                          .Query()
                          .Include(lp => lp.Artist) // Nạp các playlist từ Library_Playlist
                          .LoadAsync();

            // Trả về danh sách playlist
            return library.Library_Artist.Select(lp => lp.Artist);  // Không cần ToList() nếu trả về IEnumerable
        }


        public async Task Update(Library library)
        {
            var existingItem = await GetLibraryById(library.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(library);
            }
            else
            {
                _context.Libraries.Add(library);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid libraryId)
        {
            var library = await GetLibraryById(libraryId);
            if (library != null)
            {
                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
        }
    }

}