using BusinessObjects;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private readonly VibeZDbContext _context;
        public AlbumRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Album>> GetAllAlbumsByArtistId(Guid artistId)
        {
            return await _context.Albums.Where(x => x.ArtistId == artistId).AsNoTrackingWithIdentityResolution().Include(x => x.Artist).ToListAsync();
        }
        public async Task<int> TotalAlbum()
        {
            return await Task.FromResult(_context.Albums.Count());
        }
        public async Task<int> CountAlbum(Guid artistId)
        {
            return await _context.Albums.CountAsync(album => album.ArtistId == artistId);
        }
        public async Task<Album> GetById(Guid albumId)
        {
            var album = await _context.Albums.AsNoTrackingWithIdentityResolution().Include(x => x.Artist).FirstOrDefaultAsync(u => u.Id == albumId);
            return album;
        }

        
    }
}
