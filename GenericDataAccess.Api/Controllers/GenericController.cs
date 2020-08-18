using System;
using System.Collections.Generic;
using System.Linq;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Api.Models;
using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("3.0")]
    [ApiController]
    [Route("/persons")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok(Enumerable.Empty<Person>());
        }
    }


    // Sample controller V1 with direct accessing the db context
    [ApiVersion("1.0")]
    [GenericControllerName]
    internal class GenericController<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly GenericDbContext _context;

        public GenericController(GenericDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TEntity>> GetAsync()
        {
            return Ok(_context.Set<TEntity>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] TEntity entity)
        {
            var result = _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.EqualsTo(entity.Id));
            if (result.IsNotNull())
            {
                throw new ProblemDetailsException(404, $"Resource with id: '{entity.Id}' already exists", $"The resource of type: {typeof(TEntity).Name} with the id: '{entity.Id}' does already exists");
            }

            _context.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            var guid = new Guid(id);
            var result = _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.EqualsTo(guid));
            return result.IsNull() ?  NotFound(id).Cast<IActionResult>() : Ok();
        }
    }
}