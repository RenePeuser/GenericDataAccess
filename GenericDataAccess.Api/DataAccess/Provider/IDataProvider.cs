using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Models;

namespace Api.DataAccess.Provider
{
    internal interface IDataProvider
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : EntityBase;
        Task AddAsync<TEntity>(params TEntity[] entities) where TEntity : EntityBase;
        Task Delete<TEntity>(params TEntity[] entities) where TEntity : EntityBase;
        Task UpdateAsync<TEntity>(params TEntity[] entities) where TEntity : EntityBase;
    }
}