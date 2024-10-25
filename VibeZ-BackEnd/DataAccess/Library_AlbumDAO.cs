using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Library_AlbumDAO : SingletonBase<Library_AlbumDAO>
    {
        public async Task<IEnumerable<Library_Album>> GetAllLibrarieAlbums()
        {
            return await _context.Library_Albums.ToListAsync();
        }

        public async Task<Library_Album> GetLibraryAlbumById(Guid albumId, Guid LibraryId)
        {
            var lbA = await _context.Library_Albums.FirstOrDefaultAsync(f => f.AlbumId == albumId && f.LibraryId == LibraryId);
            if (lbA == null) return null;
            return lbA;
        }

        public async Task Add(Library_Album library_Album)
        {
            await _context.Library_Albums.AddAsync(library_Album);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Library_Album library_Album)
        {
            var existingItem = await GetLibraryAlbumById(library_Album.AlbumId, library_Album.LibraryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(library_Album);
            }
            else
            {
                await Add(library_Album); // Reuse the Add method if it doesn't exist
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid albumId, Guid libraryId)
        {
            var library_Album = await GetLibraryAlbumById(albumId, libraryId);
            if (library_Album != null)
            {
                _context.Library_Albums.Remove(library_Album);
                await _context.SaveChangesAsync();
            }
        }
    }
}