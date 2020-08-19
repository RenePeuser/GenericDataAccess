using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;

namespace Api.DataAccess.Repositories.Simple
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly GenericDbContextV3 _genericDbContextV3;

        public Repository(GenericDbContextV3 genericDbContextV3)
        {
            _genericDbContextV3 = genericDbContextV3;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(_genericDbContextV3.Set<TEntity>());
        }

        public Task AddAsync(params TEntity[] entities)
        {
            _genericDbContextV3.Set<TEntity>().AddRange(entities);
            return _genericDbContextV3.SaveChangesAsync();
        }

        public Task Delete(params TEntity[] entities)
        {
            _genericDbContextV3.Set<TEntity>().RemoveRange(entities);
            return _genericDbContextV3.SaveChangesAsync();
        }

        public Task UpdateAsync(params TEntity[] entities)
        {
            _genericDbContextV3.Set<TEntity>().RemoveRange(entities);
            return _genericDbContextV3.SaveChangesAsync();
        }
    }


    // like a query pattern, but here named only for demonstration
}