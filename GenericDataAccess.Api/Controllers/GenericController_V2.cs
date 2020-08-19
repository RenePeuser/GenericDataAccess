using System.Threading.Tasks;
using Api.Controllers.Attributes;
using Api.DataAccess.Models;
using Api.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("2.0")]
    [GenericControllerName]
    internal class GenericController_V2<TEntity> : ControllerBase where TEntity : EntityBase
    {
        private readonly IRepository<TEntity> _repository;

        public GenericController_V2(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }
    }
}