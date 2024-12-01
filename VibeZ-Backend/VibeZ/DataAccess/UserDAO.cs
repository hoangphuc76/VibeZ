using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO : SingletonBase<UserDAO>
    {
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            return user;
        }
        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;
            return user;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            if (user == null) return null;
            return user;
        }
        public async Task<User> FindByNameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;
            return user;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        /*public async Task Add(string username, string password)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }*/

        public async Task Update(User user)
        {
            var existingItem = await GetUserById(user.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid userId)
        {
            var user = await GetUserById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdatePassword(Guid UserId, string newPassword)
        {
            var user = await GetUserById(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Password = newPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

}