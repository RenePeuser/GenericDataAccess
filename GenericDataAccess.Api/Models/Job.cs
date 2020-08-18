using Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
{
    [Entity]
    [ApiVersion("1.0")]
    [GenericController("v{version:apiVersion}/jobs", "JobsController")]
    internal class Job : EntityBase
    {
        public string Company { get; set; } = "Microsoft";
    }
}