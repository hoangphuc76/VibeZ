using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class Library_AlbumRepository : Repository<Library_Album>, ILibrary_AlbumRepository
    {
        private readonly VibeZDbContext _context;
        public Library_AlbumRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
       
        public async Task<Library_Album> GetLibraryAlbumById(Guid albumId, Guid LibraryId)
        {
            var lbA = await _context.Library_Albums.FirstOrDefaultAsync(f => f.AlbumId == albumId && f.LibraryId == LibraryId);
            if (lbA == null) return null;
            return lbA;
        }

    }
}