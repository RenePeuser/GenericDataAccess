using Api.Controllers;

namespace Api.Models
{
    [Entity]
    [GenericController("api/v{version:apiVersion}/jobs", "JobsController")]
    internal class Job : EntityBase
    {
        public string Company { get; set; } = "Microsoft";
    }
}