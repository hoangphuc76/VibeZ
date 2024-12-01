using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class Library_ArtistRepository : ILibrary_ArtistRepository
    {
        public async Task<IEnumerable<Library_Artist>> GetAll() => await Library_ArtistDAO.Instance.GetAll();
        public async Task<Library_Artist> GetArtistById(Guid artistId, Guid LibraryId) => await Library_ArtistDAO.Instance.GetArtistById(artistId, LibraryId);
        public async Task Add(Library_Artist library_Artist) => await Library_ArtistDAO.Instance.Add(library_Artist);
        public async Task Update(Library_Artist library_Artist) => await Library_ArtistDAO.Instance.Update(library_Artist);
        public async Task Delete(Guid artistId, Guid libraryId) => await Library_ArtistDAO.Instance.Delete(artistId, libraryId);
    }
}
