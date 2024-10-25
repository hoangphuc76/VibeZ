using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackages();
        Task<Package> GetPackageById(Guid packageId);
        Task AddPackage(Package package);
        Task UpdatePackage(Package package);
        Task DeletePackage(Guid packageId);
    }

}
