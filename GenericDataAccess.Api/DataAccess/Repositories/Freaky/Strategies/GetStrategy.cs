using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;

namespace Api.DataAccess.Repositories.Freaky.Strategies
{
    // This sample what we do here is just a POC to demonstrate that it would possible to fetch
    // data from multiple data base source with different strategies for get, add, delete and
    // update. Do not try this at home if you not know what you are doing.
    internal class GetStrategy<TEntity> where TEntity : EntityBase
    {
        private readonly IEnumerable<GenericDbContext> _genericDbContexts;

        public GetStrategy(IEnumerable<GenericDbContext> genericDbContexts)
        {
            _genericDbContexts = genericDbContexts;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_genericDbContexts.SelectMany(db => db.Set<TEntity>()));
        }
    }
}