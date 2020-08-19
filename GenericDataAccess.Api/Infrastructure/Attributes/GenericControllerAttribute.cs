using System;

namespace Api.Controllers.Attributes
{
    public class GenericControllerAttribute : Attribute
    {
        public GenericControllerAttribute(string route, string contollerName)
        {
            Route = route;
            ContollerName = contollerName;
        }

        public string Route { get; }

        public string ContollerName { get; }
    }
}