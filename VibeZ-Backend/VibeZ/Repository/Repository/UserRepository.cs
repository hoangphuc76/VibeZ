using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly VibeZDbContext _context;
        public UserRepository( VibeZDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> TotalUser()
        {
            return await Task.FromResult(_context.Users.Count());
        }
     

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            if (user == null) return null;
            return user;
        }

    

        public async Task<Guid> GenerateUniqueUserIdAsync()
        {
            Guid userId;
            User exists;
            do
            {
                userId = Guid.NewGuid();
                exists = await GetById(userId);
            } while (exists != null);
            return userId;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            return user;
        }

   

      
        public async Task<User> FindByNameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;
            return user;
        }
        public async Task UpdatePassword(Guid UserId, string newPassword)
        {
            var user = await GetById(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Password = newPassword;
            _context.Users.Update(user);
        }
    }
}