using System.Collections.Generic;
using Api.DataAccess.Models;

namespace Api.DataAccess.Provider
{
    public interface IGenericControllerAttributeProvider
    {
        IEnumerable<GenerateControllerInfo> GetAll();
    }
}