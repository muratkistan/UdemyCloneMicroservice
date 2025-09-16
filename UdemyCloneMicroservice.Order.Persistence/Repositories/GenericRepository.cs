using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyCloneMicroservice.Order.Application.Contracts.Repositories;
using UdemyCloneMicroservice.Order.Domain.Entities;

namespace UdemyCloneMicroservice.Order.Persistence.Repositories
{
    public class GenericRepository<TId, TEntity>(AppDbContext context) : IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    {
        protected AppDbContext Context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public Task<bool> AnyAsync(TId id)
        {
            return _dbSet.AnyAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public ValueTask<TEntity?> GetByIdAsync(TId id)
        {
            return _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}