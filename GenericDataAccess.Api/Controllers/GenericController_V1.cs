using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataAccess.Mapper;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;
using Api.Infrastructure.Attributes;
using Api.Infrastructure.Errorhandling;using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // Sample controller V1 with direct accessing the db dbContext
    [ApiVersion("1.0")]
    [GenericControllerName]
    internal class GenericController_V1<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly GenericDbContextV1 _dbContext;
        private readonly IMapper<TEntity> _mapper;

        public GenericController_V1(GenericDbContextV1 dbContext, IMapper<TEntity> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TEntity>> GetAsync()
        {
            return Ok(_dbContext.Set<TEntity>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Post([FromBody] TEntity entity)
        {
            var result = await _dbContext.FindAsync<TEntity>(entity.Id);
            if (result.IsNotNull())
            {
                throw new ProblemDetailsException(404, $"Resource with id: '{entity.Id}' already exists", $"The resource of type: {typeof(TEntity).Name} with the id: '{entity.Id}' does already exists");
            }

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            var guid = new Guid(id);
            var existingItem = await _dbContext.FindAsync<TEntity>(guid);
            if (existingItem.IsNull())
            {
                return NotFound(id);
            }

            _dbContext.Remove(existingItem);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(string id, TEntity entity)
        {
            var guid = new Guid(id);
            var existingItem = await _dbContext.FindAsync<TEntity>(guid);
            if (existingItem.IsNull())
            {
                return NotFound(id);
            }
            
            var result = _mapper.Map(entity, existingItem);
            _dbContext.Update(result);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}