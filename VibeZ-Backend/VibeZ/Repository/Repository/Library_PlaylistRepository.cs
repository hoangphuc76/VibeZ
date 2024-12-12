using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class Library_PlaylistRepository : Repository<Library_Playlist>, ILibrary_PlaylistRepository
    {
        private readonly VibeZDbContext _context;
        public Library_PlaylistRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Library_Playlist> GetLibraryPlaylistById(Guid libraryId, Guid playlistId)
        {
            var lbp = await _context.Library_Playlists
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(f => f.LibraryId == libraryId && f.PlaylistId == playlistId);
            return lbp;
        }


    }
}