using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> FindByNameAsync(string username);
        Task<Guid> GenerateUniqueUserIdAsync();
        Task<User> FindByEmailAsync(string email);
        Task UpdatePassword(Guid UserId, string newPassword);
        Task<int> TotalUser();

    }

}