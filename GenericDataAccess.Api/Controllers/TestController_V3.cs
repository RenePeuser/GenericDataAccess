﻿using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("3.0")]
    [ApiController]
    [Route("/persons")]
    public class TestController_V3 : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok(Enumerable.Empty<Person>());
        }
    }
}