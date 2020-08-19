using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Provider
{
    public interface IApiVersionProvider
    {
        IEnumerable<ApiVersion> GetAll();
    }
}