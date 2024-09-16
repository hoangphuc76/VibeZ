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
            return await _context.U_packages.ToListAsync();
        }

        public async Task<User_package> GetUserPackageById(Guid userId, Guid packId)
        {
            var userPackage = await _context.U_packages.FirstOrDefaultAsync(up => up.UserId == userId && up.PackId == packId);
            if (userPackage == null) return null;
            return userPackage;
        }

        public async Task Add(User_package userPackage)
        {
            await _context.U_packages.AddAsync(userPackage);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User_package userPackage)
        {
            var existingItem = await GetUserPackageById(userPackage.UserId, userPackage.PackId);
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

        public async Task Delete(Guid userId, Guid packId)
        {
            var userPackage = await GetUserPackageById(userId, packId);
            if (userPackage != null)
            {
                _context.U_packages.Remove(userPackage);
                await _context.SaveChangesAsync();
            }
        }
    }

}
