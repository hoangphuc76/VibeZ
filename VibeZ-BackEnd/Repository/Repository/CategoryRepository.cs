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
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await CategoryDAO.Instance.GetAllCategories();
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await CategoryDAO.Instance.GetCategoryById(categoryId);
        }

        public async Task AddCategory(Category category)
        {
            await CategoryDAO.Instance.Add(category);
        }

        public async Task UpdateCategory(Category category)
        {
            await CategoryDAO.Instance.Update(category);
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            await CategoryDAO.Instance.Delete(categoryId);
        }
    }

}
