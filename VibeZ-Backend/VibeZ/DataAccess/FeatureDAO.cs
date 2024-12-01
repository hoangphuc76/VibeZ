using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FeatureDAO : SingletonBase<FeatureDAO>
    {

        public async Task<ICollection<Feature>> GetAllFeatures()
        {
            return await _context.Features.ToListAsync();
        }
        public async Task<Feature> GetFeaturesById(Guid id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null)
            {
                return null;
            }
            return feature;
        }
        public async Task Add(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Feature feature)
        {
            var existingItem = await GetFeaturesById(feature.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(feature);
            }
            else
            {
                _context.Features.Add(feature);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var feature = await GetFeaturesById(id);
            if (feature != null)
            {
                _context.Features.Remove(feature);
                await _context.SaveChangesAsync();
            }
        }
    }
}
