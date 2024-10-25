using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PackageRepository : IPackageRepository
    {
        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            return await PackageDAO.Instance.GetAllPackages();
        }

        public async Task<Package> GetPackageById(Guid packageId)
        {
            return await PackageDAO.Instance.GetPackageById(packageId);
        }

        public async Task AddPackage(Package package)
        {
            await PackageDAO.Instance.Add(package);
        }

        public async Task UpdatePackage(Package package)
        {
            await PackageDAO.Instance.Update(package);
        }

        public async Task DeletePackage(Guid packageId)
        {
            await PackageDAO.Instance.Delete(packageId);
        }
    }

}
