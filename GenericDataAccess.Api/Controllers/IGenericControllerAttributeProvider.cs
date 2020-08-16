using System.Collections.Generic;

namespace Api.Controllers
{
    public interface IGenericControllerAttributeProvider
    {
        IEnumerable<GenerateControllerInfo> GetAll();
    }
}