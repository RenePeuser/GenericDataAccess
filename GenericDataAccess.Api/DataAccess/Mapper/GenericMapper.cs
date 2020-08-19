using Api.DataAccess.Models;
using AutoMapper;

namespace Api.DataAccess.Mapper
{
    internal class GenericMapper<TEntity> : IMapper<TEntity> where TEntity : EntityBase
    {
        private readonly AutoMapper.Mapper _mapper;

        public GenericMapper()
        {
            var mapperconfig = new MapperConfiguration(config => config.CreateMap<TEntity, TEntity>());
            _mapper = new AutoMapper.Mapper(mapperconfig);
        }

        public TEntity Map(TEntity source, TEntity target)
        {
            return _mapper.Map(source, target);
        }
    }
}