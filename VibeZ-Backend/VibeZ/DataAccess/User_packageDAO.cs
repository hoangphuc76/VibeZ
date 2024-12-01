using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserPackageDAO : SingletonBase<UserPackageDAO>
    {

        public async Task<IEnumerable<User_package>> GetAllUserPackages()
        {
            using var _context = new VibeZDbContext();
            return await _context.U_packages.ToListAsync();
        }

        public async Task<User_package> GetUserPackageById(Guid id)
        {
            using var _context = new VibeZDbContext();
            var userPackage = await _context.U_packages.FirstOrDefaultAsync(x => x.Id == id);
            if (userPackage == null) return null;
            return userPackage;
        }
        public async Task<IEnumerable<User_package>> GetPackageByUserId(Guid userId)
        {
            using var _context = new VibeZDbContext();

            var userPackage = await _context.U_packages.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
            if (userPackage == null) return null;
            return userPackage;
        }
        public async Task<IEnumerable<User_package>> GetPackageByPackageId(Guid packId)
        {
            using var _context = new VibeZDbContext();

            var userPackage = await _context.U_packages.Where(x => x.PackageId == packId).AsNoTracking().ToListAsync();
            if (userPackage == null) return null;
            return userPackage;
        }

        public async Task Add(User_package userPackage)
        {
            using var _context = new VibeZDbContext();

            await _context.U_packages.AddAsync(userPackage);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User_package userPackage)
        {
            using var _context = new VibeZDbContext();

            var existingItem = await GetUserPackageById(userPackage.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(userPackage);
            }
            else
            {
                _context.U_packages.Add(userPackage);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            using var _context = new VibeZDbContext();

            var userPackage = await GetUserPackageById(id);
            if (userPackage != null)
            {
                _context.U_packages.Remove(userPackage);
                await _context.SaveChangesAsync();
            }
        }
    }

}
