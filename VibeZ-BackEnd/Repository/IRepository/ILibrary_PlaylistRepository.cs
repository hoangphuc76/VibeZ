using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_PlaylistRepository
    {
        Task<IEnumerable<Library_Playlist>> GetAllLibrariePlaylists();
        Task<Library_Playlist> GetLibraryPlaylistById(Guid libraryId, Guid trackId);
        Task Add(Library_Playlist library_Playlist);
        Task Update(Library_Playlist library_Playlist);
        Task Delete(Guid libraryId, Guid playlistId);
    }
}