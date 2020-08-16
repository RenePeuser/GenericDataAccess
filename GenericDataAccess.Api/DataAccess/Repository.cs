using System.Collections.Generic;
using Api.DataAccess.Provider;
using Api.Models;

namespace Api.DataAccess
{
    internal class Repository<TEntity> where TEntity : EntityBase
    {
        private readonly GenericDbContext _genericDbContext;

        public Repository(GenericDbContext genericDbContext)
        {
            _genericDbContext = genericDbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _genericDbContext.Set<TEntity>();
        }

        public void Add(params TEntity[] entities)
        {
            _genericDbContext.Set<TEntity>().AddRange(entities);
            _genericDbContext.SaveChanges();
        }

        public void Delete(params TEntity[] entities)
        {
            _genericDbContext.Set<TEntity>().RemoveRange(entities);
            _genericDbContext.SaveChanges();
        }
    }
}