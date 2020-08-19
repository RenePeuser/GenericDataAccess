using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;
using Api.DataAccess.Repositories.Freaky.Strategies;

namespace Api.DataAccess.Repositories.Freaky
{
    // This sample what we do here is just a POC to demonstrate that it would possible to fetch
    // data from multiple data base source with different strategies for get, add, delete and
    // update. Do not try this at home if you not know what you are doing.
    internal class StrategyRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly GetStrategy<TEntity> _getStrategy;
        private readonly AddStrategy<TEntity> _addStrategy;
        private readonly UpdateStrategy<TEntity> _updateStrategy;
        private readonly DeleteStrategy<TEntity> _deleteStrategy;

        public StrategyRepository(GetStrategy<TEntity> getStrategy, AddStrategy<TEntity> addStrategy, UpdateStrategy<TEntity> updateStrategy, DeleteStrategy<TEntity> deleteStrategy)
        {
            _getStrategy = getStrategy;
            _addStrategy = addStrategy;
            _updateStrategy = updateStrategy;
            _deleteStrategy = deleteStrategy;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _getStrategy.GetAllAsync();
        }

        public Task AddAsync(params TEntity[] entities)
        {
            return _addStrategy.AddAsync(entities);
        }

        public Task Delete(params TEntity[] entities)
        {
            return _deleteStrategy.DeleteAsync(entities);
        }

        public Task UpdateAsync(params TEntity[] entities)
        {
            return _updateStrategy.UpdateAsync(entities);
        }
    }
}