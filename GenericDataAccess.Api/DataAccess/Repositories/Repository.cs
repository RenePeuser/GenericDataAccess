using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Provider;
using Api.Models;

namespace Api.DataAccess.Repositories
{
    internal interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(params TEntity[] entities);
        Task Delete(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
    }

    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly GenericDbContext _genericDbContext;

        public Repository(GenericDbContext genericDbContext)
        {
            _genericDbContext = genericDbContext;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(_genericDbContext.Set<TEntity>());
        }

        public Task AddAsync(params TEntity[] entities)
        {
            _genericDbContext.Set<TEntity>().AddRange(entities);
            return _genericDbContext.SaveChangesAsync();
        }

        public Task Delete(params TEntity[] entities)
        {
            _genericDbContext.Set<TEntity>().RemoveRange(entities);
            return _genericDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(params TEntity[] entities)
        {
            _genericDbContext.Set<TEntity>().RemoveRange(entities);
            return _genericDbContext.SaveChangesAsync();
        }
    }
}