using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Api.DataAccess.Provider
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IGenericControllerProvider _genericControllerProvider;
        private readonly IGenericTypeProvider _genericTypeProvider;
        private readonly TypeToControllerNameProvider _typeToControllerNameProvider;

        internal GenericControllerFeatureProvider() : this(new TypeToControllerNameProvider(new GenericControllerAttributeProvider()), new GenericTypeProvider(), new GenericControllerProvider())
        {
        }

        private GenericControllerFeatureProvider(TypeToControllerNameProvider typeToControllerNameProvider, IGenericTypeProvider genericTypeProvider, IGenericControllerProvider genericControllerProvider)
        {
            _typeToControllerNameProvider = typeToControllerNameProvider;
            _genericTypeProvider = genericTypeProvider;
            _genericControllerProvider = genericControllerProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            //foreach (var type in _genericTypeProvider.GetSupportedTypes())
            //{
            //    var controllerName = _typeToControllerNameProvider.GetNameFor(type);
            //    if (!feature.Controllers.Any(t => t.Name == controllerName))
            //    {
            //        foreach (var genericControllerType in _genericControllerProvider.GetAll())
            //        {
            //            var controllerType = genericControllerType.GetTypeInfo();
            //            feature.Controllers.Add(controllerType);
            //        }
            //    }
            //}

            foreach (var genericControllerType in _genericControllerProvider.GetAll())
            {
                var controllerType = genericControllerType.GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}
