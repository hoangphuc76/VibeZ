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
    public class Library_PlaylistRepository : ILibrary_PlaylistRepository
    {
        public async Task Add(Library_Playlist library_Playlist)
        {
            await Library_PlaylistsDAO.Instance.Add(library_Playlist);
        }

        public async Task Delete(Guid libraryId, Guid playlistId)
        {
            await Library_PlaylistsDAO.Instance.Delete(libraryId, playlistId);
        }

        public async Task<IEnumerable<Library_Playlist>> GetAllLibrariePlaylists()
        {
            return await Library_PlaylistsDAO.Instance.GetAllLibrariePlaylists();
        }

        public async Task<Library_Playlist> GetLibraryPlaylistById(Guid libraryId, Guid trackId)
        {
            return await Library_PlaylistsDAO.Instance.GetLibraryPlaylistById(libraryId, trackId);
        }

        public async Task Update(Library_Playlist library_Playlist)
        {
            await Library_PlaylistsDAO.Instance.Update(library_Playlist);
        }
    }
}