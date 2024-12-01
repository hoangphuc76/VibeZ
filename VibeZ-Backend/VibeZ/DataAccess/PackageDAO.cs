using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PackageDAO : SingletonBase<PackageDAO>
    {
        public async Task<ICollection<Package>> GetAllPackages()
        {
            using var _context = new VibeZDbContext();
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetPackageById(Guid id)
        {
            using var _context = new VibeZDbContext();

            var package = await _context.Packages.FirstOrDefaultAsync(p => p.Id == id);
            if (package == null)
            {
                return null;
            }
            return package;
        }

        public async Task Add(Package package)
        {
            using var _context = new VibeZDbContext();

            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Package package)
        {
            using var _context = new VibeZDbContext();

            var existingItem = await GetPackageById(package.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(package);
            }
            else
            {
                _context.Packages.Add(package);
            }
           await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            using var _context = new VibeZDbContext();

            var package = await GetPackageById(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }

}
