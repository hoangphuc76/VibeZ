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
    public class Library_ArtistRepository : Repository<Library_Artist>, ILibrary_ArtistRepository
        
    {
        private readonly VibeZDbContext _context;
        public Library_ArtistRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Library_Artist> GetArtistById(Guid artistId, Guid LibraryId)
        {
            var libraryArtist = await _context.Library_Artists
                                  .AsNoTracking()  // Không tracking vì chỉ đọc dữ liệu
                                  .FirstOrDefaultAsync(f => f.ArtistId == artistId && f.LibraryId == LibraryId);

            return libraryArtist;
        }

    }
}
