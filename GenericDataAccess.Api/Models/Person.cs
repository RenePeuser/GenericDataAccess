using System;
using Api.Controllers.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    [Entity]
    [ApiVersion("2.0")]
    [GenericController("v{version:apiVersion}/jobs", "JobsController")]
    internal class Job : EntityBase
    {
        public string Company { get; set; } = "Microsoft";
    }

    [Entity]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [GenericController("v{version:apiVersion}/persons", "PersonsController")]
    internal class Person : EntityBase
    {
        public string Name { get; set; } = "Son";

        public string FamilyName { get; set; } = "Goku";

        public DateTime Birthday { get; set; } = new DateTime(1984, 11, 1);
    }
}