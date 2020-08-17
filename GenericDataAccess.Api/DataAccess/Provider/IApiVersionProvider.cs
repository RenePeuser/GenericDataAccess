using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Api.DataAccess.Provider
{
    public interface IApiVersionProvider
    {
        IEnumerable<ApiVersion> GetAll();
    }
}