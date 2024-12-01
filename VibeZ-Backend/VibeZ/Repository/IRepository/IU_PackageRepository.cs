using BusinessObjects;
using DataAccess;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IU_PackageRepository
    {
        Task<IEnumerable<User_package>> GetAllUserPackages();
        Task<User_package> GetUserPackageById(Guid id);
        Task AddUserPackage(User_package userPackage);
        Task UpdateUserPackage(User_package userPackage);
        Task DeleteUserPackage(Guid id);
        Task<IEnumerable<User_package>> GetPackageByUserId(Guid userId);
        Task<IEnumerable<User_package>> GetPackageByPackageId(Guid packId);
    }

}