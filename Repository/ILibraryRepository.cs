using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GetAllLibraries();
        Task<Library> GetLibraryById(Guid libraryId);
        Task AddLibrary(Library library);
        Task UpdateLibrary(Library library);
        Task DeleteLibrary(Guid libraryId);
    }
}
