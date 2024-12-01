using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class Library_AlbumRepository : ILibrary_AlbumRepository
    {
        public async Task Add(Library_Album library_Album)
        {
            await Library_AlbumDAO.Instance.Add(library_Album);
        }

        public async Task Delete(Guid albumId, Guid libraryId)
        {
            await Library_AlbumDAO.Instance.Delete(albumId, libraryId);
        }

        public async Task<IEnumerable<Library_Album>> GetAllLibrarieAlbums()
        {
            return await Library_AlbumDAO.Instance.GetAllLibrarieAlbums();
        }

        public async Task<Library_Album> GetLibraryAlbumById(Guid albumId, Guid LibraryId)
        {
            return await Library_AlbumDAO.Instance.GetLibraryAlbumById(albumId, LibraryId);
        }

        public async Task Update(Library_Album library_Album)
        {
            await Library_AlbumDAO.Instance.Update(library_Album);
        }
    }
}