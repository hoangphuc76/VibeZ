using BusinessObjects;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly VibeZDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(VibeZDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public Task Update(T entity)
        {
            _entities.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }
    }
}
