using System.Collections.Generic;
using Api.DataAccess.Models;
using Api.Infrastructure.Models;

namespace Api.Infrastructure.Provider
{
    public interface IGenericControllerAttributeProvider
    {
        IEnumerable<GenerateControllerInfo> GetAll();
    }
}