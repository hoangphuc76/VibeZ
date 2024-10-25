using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Library_ArtistDAO : SingletonBase<Library_ArtistDAO>
    {
        public async Task<IEnumerable<Library_Artist>> GetAll()
        {
            return await _context.Library_Artists.ToListAsync();
        }

        public async Task<Library_Artist> GetArtistById(Guid artistId, Guid LibraryId)
        {
            var lbA = await _context.Library_Artists.FirstOrDefaultAsync(f => f.ArtistId == artistId && f.LibraryId == LibraryId);
            if (lbA == null) return null;
            return lbA;
        }

        public async Task Add(Library_Artist library_Artist)
        {
            await _context.Library_Artists.AddAsync(library_Artist);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Library_Artist library_Artist)
        {
            var existingItem = await GetArtistById(library_Artist.ArtistId, library_Artist.LibraryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(library_Artist);
            }
            else
            {
                await Add(library_Artist); // Reuse the Add method if it doesn't exist
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid artistId, Guid libraryId)
        {
            var library_Artist = await GetArtistById(artistId, libraryId);
            if (library_Artist != null)
            {
                _context.Library_Artists.Remove(library_Artist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
