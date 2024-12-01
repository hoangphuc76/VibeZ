using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid userId);
        //Task AddUser(string username, string password);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid userId);
        Task<User> Authenticate(string username, string password);
        Task<User> FindByNameAsync(string username);
        Task<Guid> GenerateUniqueUserIdAsync();
        Task<User> FindByEmailAsync(string email);
        Task AddUserGoogle(User user);
        Task UpdatePassword(Guid UserId, string newPassword);
        Task<int> TotalUser();

    }

}