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
    public class P_FeaturesRepository : IP_FeaturesRepository
    {
        public async Task<IEnumerable<Package_features>> GetAllPackageFeatures()
        {
            return await PackageFeaturesDAO.Instance.GetAllPackageFeatures();
        }

        public async Task<Package_features> GetPackageFeatureById(Guid packId, Guid featureId)
        {
            return await PackageFeaturesDAO.Instance.GetPackageFeatureById(packId, featureId);
        }

        public async Task AddPackageFeature(Package_features packageFeature)
        {
            await PackageFeaturesDAO.Instance.Add(packageFeature);
        }

        public async Task UpdatePackageFeature(Package_features packageFeature)
        {
            await PackageFeaturesDAO.Instance.Update(packageFeature);
        }

        public async Task DeletePackageFeature(Guid packId, Guid featureId)
        {
            await PackageFeaturesDAO.Instance.Delete(packId, featureId);
        }
    }
}
