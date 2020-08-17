using Api.DataAccess;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("2.0")]
    [GenericControllerName]
    internal class GenericControllerV2<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly Repository<TEntity> _repository;

        public GenericControllerV2(Repository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            return Ok(_repository.GetAll());
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] TEntity entity)
        //{
        //    _repository.Add(entity);
        //    return Ok(entity);
        //}

        //[HttpDelete]
        //public IActionResult Delete(string id)
        //{
        //    _repository.Delete(id);
        //    return Ok();
        //}
    }
}