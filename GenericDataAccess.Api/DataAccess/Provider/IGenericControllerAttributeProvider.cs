using System.Collections.Generic;
using Api.Controllers;
using Api.DataAccess.Models;

namespace Api.DataAccess.Provider
{
    public interface IGenericControllerAttributeProvider
    {
        IEnumerable<GenerateControllerInfo> GetAll();
    }
}