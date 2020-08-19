using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Mapper;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;
using Extensions.Pack;

namespace Api.DataAccess.Repositories.Freaky.Strategies
{
    // This sample what we do here is just a POC to demonstrate that it would possible to fetch
    // data from multiple data base source with different strategies for get, add, delete and
    // update. Do not try this at home if you not know what you are doing.
    internal class UpdateStrategy<TEntity> where TEntity : EntityBase
    {
        private readonly IEnumerable<GenericDbContext> _genericDbContexts;
        private readonly IMapper<TEntity> _mapper;

        public UpdateStrategy(IEnumerable<GenericDbContext> genericDbContexts, IMapper<TEntity> mapper)
        {
            _genericDbContexts = genericDbContexts;
            _mapper = mapper;
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            foreach (var genericDbContext in _genericDbContexts)
            {
                foreach (var entity in entities)
                {
                    var existingItem = await genericDbContext.FindAsync<TEntity>(entity.Id);
                    if (existingItem.IsNull())
                    {
                        // Decide what is to do, maybe an update or update style.
                        continue;
                    }

                    var result = _mapper.Map(entity, existingItem);
                    genericDbContext.Update(result);

                    await genericDbContext.SaveChangesAsync();
                }
            }
        }
    }
}