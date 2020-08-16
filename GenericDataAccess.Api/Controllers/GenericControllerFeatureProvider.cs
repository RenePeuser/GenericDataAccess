using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Api.Controllers
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly GenericTypeProvider _genericTypeProvider;
        private readonly TypeToControllerNameProvider _typeToControllerNameProvider;

        internal GenericControllerFeatureProvider() : this(new TypeToControllerNameProvider(new GenericControllerAttributeProvider()),
            new GenericTypeProvider(new GenericControllerAttributeProvider()))
        {
        }

        private GenericControllerFeatureProvider(TypeToControllerNameProvider typeToControllerNameProvider, GenericTypeProvider genericTypeProvider)
        {
            _typeToControllerNameProvider = typeToControllerNameProvider;
            _genericTypeProvider = genericTypeProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _genericTypeProvider.GetSupportedTypes())
            {
                var controllerName = _typeToControllerNameProvider.GetNameFor(type);
                if (!feature.Controllers.Any(t => t.Name == controllerName))
                {
                    var controllerType = typeof(GenericController<>).MakeGenericType(type).GetTypeInfo();
                    feature.Controllers.Add(controllerType);
                }
            }
        }
    }
}