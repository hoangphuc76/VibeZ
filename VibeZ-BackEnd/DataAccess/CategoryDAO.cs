using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO : SingletonBase<CategoryDAO>
    {
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid cateogryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(l => l.CategoryId == cateogryId);
            if (category == null) return null;
            return category;
        }

        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            var existingItem = await GetCategoryById(category.CategoryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Add(category);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid cateogryId)
        {
            var category = await GetCategoryById(cateogryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }

}
