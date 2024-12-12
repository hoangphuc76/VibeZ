using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> FindByNameAsync(string username);
        Task<Guid> GenerateUniqueUserIdAsync();
        Task<User> FindByEmailAsync(string email);
        Task AddUserGoogle(User user);
        Task AddUser(User user);

        Task UpdatePassword(Guid userId, string newPassword);
        Task<int> TotalUser();
    }
}
