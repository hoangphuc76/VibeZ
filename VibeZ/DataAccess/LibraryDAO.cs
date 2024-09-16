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

        public async Task<Library> GetLibraryById(Guid libraryId)
        {
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == libraryId);
            if (library == null) return null;
            return library;
        }

        public async Task Add(Library library)
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
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
