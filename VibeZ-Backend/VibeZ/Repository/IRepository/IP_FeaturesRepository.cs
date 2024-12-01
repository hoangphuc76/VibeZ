using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IP_FeaturesRepository
    {
        Task<IEnumerable<Package_features>> GetAllPackageFeatures();
        Task<Package_features> GetPackageFeatureById(Guid packId, Guid featureId);
        Task AddPackageFeature(Package_features packageFeature);
        Task UpdatePackageFeature(Package_features packageFeature);
        Task DeletePackageFeature(Guid packId, Guid featureId);
    }

}
