using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application;
using VotingSystem.Infrastructure.Data.Context;

namespace VotingSystem.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
    
        private readonly VotingSystemContext _dbContext;

        public Repository(VotingSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>()
                .ToListAsync();
        }

        public async Task<int> GetTotalCount()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
        {
            return await GetPagedResponseQuery(page, size)
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>()
                .FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>()
                .AddAsync(entity);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void DeleteAsync(T entity)
        {
            _dbContext.Set<T>()
                .Remove(entity);
        }

        protected IQueryable<T> GetPagedResponseQuery(int page, int size)
        {
            return _dbContext.Set<T>()
                .Skip((page - 1) * size)
                .Take(size);
        }
    }
}
