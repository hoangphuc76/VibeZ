using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PackageFeaturesDAO : SingletonBase<PackageFeaturesDAO>
    {
        public async Task<IEnumerable<Package_features>> GetAllPackageFeatures()
        {
            return await _context.P_features.ToListAsync();
        }

        public async Task<Package_features> GetPackageFeatureById(Guid packId, Guid featureId)
        {
            var packageFeature = await _context.P_features.FirstOrDefaultAsync(pf => pf.PackId == packId && pf.FeatureId == featureId);
            if (packageFeature == null) return null;
            return packageFeature;
        }

        public async Task Add(Package_features packageFeature)
        {
            await _context.P_features.AddAsync(packageFeature);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Package_features packageFeature)
        {
            var existingItem = await GetPackageFeatureById(packageFeature.PackId, packageFeature.FeatureId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(packageFeature);
            }
            else
            {
                _context.P_features.Add(packageFeature);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid packId, Guid featureId)
        {
            var packageFeature = await GetPackageFeatureById(packId, featureId);
            if (packageFeature != null)
            {
                _context.P_features.Remove(packageFeature);
                await _context.SaveChangesAsync();
            }
        }
    }

}
