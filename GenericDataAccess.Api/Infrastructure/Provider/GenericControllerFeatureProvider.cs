using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Api.Infrastructure.Provider
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IGenericControllerProvider _genericControllerProvider;

        internal GenericControllerFeatureProvider() : this(new GenericControllerProvider())
        {
        }

        private GenericControllerFeatureProvider(IGenericControllerProvider genericControllerProvider)
        {
            _genericControllerProvider = genericControllerProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var genericControllerType in _genericControllerProvider.GetAll())
            {
                var controllerType = genericControllerType.GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}