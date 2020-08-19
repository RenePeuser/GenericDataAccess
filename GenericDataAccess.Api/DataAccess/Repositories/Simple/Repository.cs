using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;

namespace Api.DataAccess.Repositories.Simple
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly InMemoryDbContext _inMemoryDbContext;

        public Repository(InMemoryDbContext inMemoryDbContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(_inMemoryDbContext.Set<TEntity>());
        }

        public Task AddAsync(params TEntity[] entities)
        {
            _inMemoryDbContext.Set<TEntity>().AddRange(entities);
            return _inMemoryDbContext.SaveChangesAsync();
        }

        public Task Delete(params TEntity[] entities)
        {
            _inMemoryDbContext.Set<TEntity>().RemoveRange(entities);
            return _inMemoryDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(params TEntity[] entities)
        {
            _inMemoryDbContext.Set<TEntity>().RemoveRange(entities);
            return _inMemoryDbContext.SaveChangesAsync();
        }
    }


    // like a query pattern, but here named only for demonstration
}