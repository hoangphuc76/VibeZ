

using BusinessObjects;
using DataAccess;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public class U_PackageRepository : IU_PackageRepository
    {
        public async Task<IEnumerable<User_package>> GetAllUserPackages()
        {
            return await UserPackageDAO.Instance.GetAllUserPackages();
        }

        public async Task<User_package> GetUserPackageById(Guid id)
        {
            return await UserPackageDAO.Instance.GetUserPackageById(id);
        }
        public async Task<IEnumerable<User_package>> GetPackageByUserId(Guid userId)
        {
            return await UserPackageDAO.Instance.GetPackageByUserId(userId);
        }
        public async Task<IEnumerable<User_package>> GetPackageByPackageId(Guid packId)
        {
            return await UserPackageDAO.Instance.GetPackageByPackageId(packId);

        }

        public async Task AddUserPackage(User_package userPackage)
        {
            await UserPackageDAO.Instance.Add(userPackage);
        }

        public async Task UpdateUserPackage(User_package userPackage)
        {
            await UserPackageDAO.Instance.Update(userPackage);
        }

        public async Task DeleteUserPackage(Guid id)
        {
            await UserPackageDAO.Instance.Delete(id);
        }
    }

}
