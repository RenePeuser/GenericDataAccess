using System;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    [Entity]
    [ApiVersion("1.0")]
    [GenericController("v{version:apiVersion}/persons", "PersonsController")]
    internal class Person : EntityBase
    {
        public string Name { get; set; } = "Son";

        public string FamilyName { get; set; } = "Goku";

        public DateTime Birthday { get; set; } = new DateTime(1984, 11, 1);
    }
}