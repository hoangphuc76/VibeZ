using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
        private readonly VibeZDbContext _context;
        public LibraryRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsByLibraryId(Guid libraryId)
        {
            // Lấy Library cùng với liên kết Playlist trong một truy vấn
            var library = await _context.Libraries
                                        .Include(l => l.Library_Playlists)
                                        .ThenInclude(lp => lp.Playlist)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Playlists.Select(lp => lp.Playlist) ?? new List<Playlist>();
        }
        public async Task<IEnumerable<Artist>> GetArtistByLibraryId(Guid libraryId)
        {
            var library = await _context.Libraries
                                       .Include(l => l.Library_Artist)
                                       .ThenInclude(la => la.Artist)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Artist.Select(la => la.Artist) ?? new List<Artist>();
        }
        public async Task<IEnumerable<Album>> GetAlbumsByLibraryId(Guid libraryId)
        {
            var library = await _context.Libraries
                                                    .Include(l => l.Library_Albums)
                                                    .ThenInclude(la => la.Album) // Sử dụng ThenInclude để tải album
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(l => l.Id == libraryId);

            return library?.Library_Albums.Select(la => la.Album) ?? new List<Album>();
        }
    }
}
