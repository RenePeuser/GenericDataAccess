using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;

namespace Api.DataAccess.Repositories.Freaky.Strategies
{
    // This sample what we do here is just a POC to demonstrate that it would possible to fetch
    // data from multiple data base source with different strategies for get, add, delete and
    // update. Do not try this at home if you not know what you are doing.
    internal class AddStrategy<TEntity> where TEntity : EntityBase
    {
        private readonly IEnumerable<GenericDbContextBase> _genericDbContexts;


        // this is only because it is not possible today to do multiple registrations on one type to get same instance.
        // service.AddSingleton<GenericDbContextV1>()
        // service.AddSingleton<GenericDbContextBase, GenericDbContextV1>();
        // this sample will return every time a new instance but we want for this test the same instance.
        public AddStrategy(GenericDbContextV1 genericDbContextV1, GenericDbContextV2 genericDbContextV2, GenericDbContextV3 genericDbContextV3) : this(genericDbContextV3, genericDbContextV2, genericDbContextV1)
        {
        }

        private AddStrategy(params GenericDbContextBase[] genericDbContexts)
        {
            _genericDbContexts = genericDbContexts;
        }



        public async Task AddAsync(params TEntity[] entities)
        {
            foreach (var genericDbContext in _genericDbContexts)
            {
                await genericDbContext.AddAsync(entities);
            }
        }
    }
}