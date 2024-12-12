using BusinessObjects;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IU_PackageRepository : IRepository<User_package>
    {
        Task<IEnumerable<User_package>> GetPackageByUserId(Guid userId);
        Task<IEnumerable<User_package>> GetPackageByPackageId(Guid packId);
        Task<User_package> GetUserByPackageId(Guid packId);

    }

}