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
    public class FeatureRepository : IFeatureRepository
    {
        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return await FeatureDAO.Instance.GetAllFeatures();
        }

        public async Task<Feature> GetFeatureById(Guid featureId)
        {
            return await FeatureDAO.Instance.GetFeaturesById(featureId);
        }

        public async Task AddFeature(Feature feature)
        {
            await FeatureDAO.Instance.Add(feature);
        }

        public async Task UpdateFeature(Feature feature)
        {
            await FeatureDAO.Instance.Update(feature);
        }

        public async Task DeleteFeature(Guid featureId)
        {
            await FeatureDAO.Instance.Delete(featureId);
        }
    }

}
