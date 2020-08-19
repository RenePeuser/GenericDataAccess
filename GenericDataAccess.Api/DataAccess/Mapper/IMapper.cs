using Api.DataAccess.Models;

namespace Api.DataAccess.Mapper
{
    internal interface IMapper<TEntity> where TEntity : EntityBase
    {
        TEntity Map(TEntity source, TEntity target);
    }
}