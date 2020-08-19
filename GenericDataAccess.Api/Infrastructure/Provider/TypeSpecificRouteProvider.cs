using System;
using System.Linq;
using Api.Infrastructure.Errorhandling;
using Extensions.Pack;

namespace Api.Infrastructure.Provider
{
    public class TypeSpecificRouteProvider
    {
        private readonly IGenericControllerAttributeProvider _genericControllerAttributeProvider;

        public TypeSpecificRouteProvider() : this(new GenericControllerAttributeProvider())
        {
        }

        public TypeSpecificRouteProvider(IGenericControllerAttributeProvider genericControllerAttributeProvider)
        {
            _genericControllerAttributeProvider = genericControllerAttributeProvider;
        }

        public string GetRouteFor(Type type)
        {
            // First check for generic controller attributes
            var genericControllerInfo = _genericControllerAttributeProvider.GetAll().FirstOrDefault(attribute => attribute.Type.EqualsTo(type));
            if (genericControllerInfo.IsNotNull())
            {
                return genericControllerInfo.GenericControllerAttribute.Route;
            }

            throw new ProblemDetailsException(500, $"Unknown type: '{type.Name}' to calculate a controller name.", $"Can not find a type name mapping for given entity type: {type.Name}");
        }
    }
}