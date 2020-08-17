using System.Collections.Generic;
using Api.Controllers;

namespace Api.DataAccess.Provider
{
    public interface IGenericControllerAttributeProvider
    {
        IEnumerable<GenerateControllerInfo> GetAll();
    }
}