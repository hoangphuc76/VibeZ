using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ILibrary_PlaylistRepository : IRepository<Library_Playlist>
    {
        Task<Library_Playlist> GetLibraryPlaylistById(Guid libraryId, Guid trackId);

    }
}