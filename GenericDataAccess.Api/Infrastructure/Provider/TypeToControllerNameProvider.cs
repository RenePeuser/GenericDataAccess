using System;
using System.Linq;
using Api.Infrastructure.Errorhandling;

namespace Api.Infrastructure.Provider
{
    public class TypeToControllerNameProvider
    {
        private readonly IGenericControllerAttributeProvider _genericControllerAttributeProvider;

        public TypeToControllerNameProvider() : this(new GenericControllerAttributeProvider())
        {
        }

        public TypeToControllerNameProvider(IGenericControllerAttributeProvider genericControllerAttributeProvider)
        {
            _genericControllerAttributeProvider = genericControllerAttributeProvider;
        }

        internal string GetNameFor(Type type)
        {
            // First check for generic controller attributes
            var genericControllerInfo = _genericControllerAttributeProvider.GetAll().FirstOrDefault(attribute => attribute.Type == type);
            if (genericControllerInfo != null)
            {
                return genericControllerInfo.GenericControllerAttribute.ContollerName;
            }

            throw new ProblemDetailsException(500, $"Unknown type: '{type.Name}' to find a route for.", "Can not find a route mapping for given entity type");
        }
    }
}