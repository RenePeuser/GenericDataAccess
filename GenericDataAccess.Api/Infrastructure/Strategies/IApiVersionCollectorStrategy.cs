using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Strategies
{
    public interface IApiVersionCollectorStrategy
    {
        IEnumerable<ApiVersion> Collect();
    }
}