using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers.Attributes;
using Api.DataAccess.Mapper;
using Api.DataAccess.Models;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // Sample controller V1 with direct accessing the db context
    [ApiVersion("1.0")]
    [GenericControllerName]
    internal class GenericController_V1<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly GenericDbContext _context;
        private readonly IMapper<TEntity> _mapper;

        public GenericController_V1(GenericDbContext context, IMapper<TEntity> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TEntity>> GetAsync()
        {
            return Ok(_context.Set<TEntity>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Post([FromBody] TEntity entity)
        {
            var result = await _context.FindAsync<TEntity>(entity.Id);
            if (result.IsNotNull())
            {
                throw new ProblemDetailsException(404, $"Resource with id: '{entity.Id}' already exists", $"The resource of type: {typeof(TEntity).Name} with the id: '{entity.Id}' does already exists");
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            var guid = new Guid(id);
            var existingItem = _context.FindAsync<TEntity>(guid);
            if (existingItem.IsNull())
            {
                return NotFound(id);
            }

            _context.Remove(existingItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(string id, TEntity entity)
        {
            var guid = new Guid(id);
            var existingItem = await _context.FindAsync<TEntity>(guid);
            if (existingItem.IsNull())
            {
                return NotFound(id);
            }

            var result = _mapper.Map(entity, existingItem);
            _context.Update(result);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}