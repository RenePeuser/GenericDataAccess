using System;
using Api.Attributes;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    [Entity]
    [ApiVersion("3.0")]
    [GenericController("v{version:apiVersion}/persons", "PersonsController")]
    internal class Person : EntityBase
    {
        public string Name { get; set; } = "Son";

        public string FamilyName { get; set; } = "Goku";

        public DateTime Birthday { get; set; } = new DateTime(1984, 11, 1);
    }

    [Entity]
    [ApiVersion("2.0")]
    [GenericController("v{version:apiVersion}/company", "CompaniesController")]
    internal class Company : EntityBase
    {
        public string Name { get; set; } = "Son";
    }
}