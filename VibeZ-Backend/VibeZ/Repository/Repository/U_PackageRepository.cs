

using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public class U_PackageRepository : Repository<User_package>, IU_PackageRepository
    {
        private readonly VibeZDbContext _context;

        public U_PackageRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
  
        public async Task<User_package> GetUserByPackageId(Guid id)
        {
            var userPackage = await _context.U_packages.FirstOrDefaultAsync(x => x.Id == id);
            if (userPackage == null) return null;
            return userPackage;
        }
        public async Task<IEnumerable<User_package>> GetPackageByUserId(Guid userId)
        {
            var userPackage = await _context.U_packages.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
            if (userPackage == null) return null;
            return userPackage;
        }
        public async Task<IEnumerable<User_package>> GetPackageByPackageId(Guid packId)
        {
            var userPackage = await _context.U_packages.Where(x => x.PackageId == packId).AsNoTracking().ToListAsync();
            if (userPackage == null) return null;
            return userPackage;
        }

    }

}
