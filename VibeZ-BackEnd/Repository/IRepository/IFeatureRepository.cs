using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<Feature> GetFeatureById(Guid featureId);
        Task AddFeature(Feature feature);
        Task UpdateFeature(Feature feature);
        Task DeleteFeature(Guid featureId);
    }

}
