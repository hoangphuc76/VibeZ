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
    public class ArtistPendingRepository : IArtistPendingRepository
    {
        private readonly VibeZDbContext _context;
        public ArtistPendingRepository(VibeZDbContext context)
        {
            _context = context;
        }
        public async Task Add(ArtistPending artistPending)
        {
            _context.ArtistPendings.Add(artistPending);
            await _context.SaveChangesAsync();
        }
        public async Task Update(ArtistPending artistPending)
        {
            _context.ArtistPendings.Update(artistPending);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(ArtistPending artistPending)
        {
            _context.ArtistPendings.Remove(artistPending);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ArtistPending>> GetAll()
        {
            return await _context.ArtistPendings.ToListAsync();
        }
        public async Task<ArtistPending> GetById(Guid id)
        {
            return await _context.ArtistPendings.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
