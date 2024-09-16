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
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetPackageById(Guid id)
        {
            var package = await _context.Packages.FirstOrDefaultAsync(p => p.Id == id);
            if (package == null)
            {
                return null;
            }
            return package;
        }

        public async Task Add(Package package)
        {
            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Package package)
        {
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
            var package = await GetPackageById(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }

}
