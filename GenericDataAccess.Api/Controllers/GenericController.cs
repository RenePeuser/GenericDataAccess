using System;
using System.Linq;
using Api.Attributes;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Api.Models;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        public IActionResult GetAsync()
        {
            return Ok(_context.Set<TEntity>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            var result = _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.EqualsTo(entity.Id));
            if (result.IsNotNull())
            {
                throw new ProblemDetailsException(404, $"Resource with id: '{entity.Id}' already exists",
                    $"The resource of type: {typeof(TEntity).Name} with the id: '{entity.Id}' does already exists");
            }

            _context.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var guid = new Guid(id);
            var result = _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.EqualsTo(guid));
            if (result.IsNull())
            {
                throw new ProblemDetailsException(404, $"Resource with id: '{id}' does not exists", $"The resource of type: {typeof(TEntity).Name} with the id: '{id}' does not exists");
            }

            _context.Remove(result);
            _context.SaveChanges();

            return Ok();
        }
    }
}