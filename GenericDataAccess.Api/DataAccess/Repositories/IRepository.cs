using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;

namespace Api.DataAccess.Repositories
{
    internal interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(params TEntity[] entities);
        Task Delete(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
    }
}