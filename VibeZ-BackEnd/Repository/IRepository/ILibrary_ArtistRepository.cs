using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_ArtistRepository
    {
        Task<IEnumerable<Library_Artist>> GetAll();
        Task<Library_Artist> GetArtistById(Guid artistId, Guid LibraryId);
        Task Add(Library_Artist library_Artist);
        Task Update(Library_Artist library_Artist);
        Task Delete(Guid artist, Guid libraryId);
    }
}