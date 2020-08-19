using Api.Controllers.Attributes;
using Api.DataAccess;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("2.0")]
    [GenericControllerName]
    internal class GenericController_V2<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly Repository<TEntity> _repository;

        public GenericController_V2(Repository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAsync()
        {
            return Ok(_repository.GetAll());
        }
    }
}