using System;

namespace Api.Controllers
{
    public class GenerateControllerInfo
    {
        public GenerateControllerInfo(Type type, GenericControllerAttribute genericControllerAttribute)
        {
            Type = type;
            GenericControllerAttribute = genericControllerAttribute;
        }

        public Type Type { get; }

        public GenericControllerAttribute GenericControllerAttribute { get; }
    }
}