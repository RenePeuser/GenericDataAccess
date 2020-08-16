using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    public class GenericTypeProvider
    {
        private readonly IGenericControllerAttributeProvider _genericControllerAttributeProvider;
        private readonly Lazy<IEnumerable<Type>> _supportedTypes;

        public GenericTypeProvider() : this(new GenericControllerAttributeProvider())
        {
        }

        public GenericTypeProvider(IGenericControllerAttributeProvider genericControllerAttributeProvider)
        {
            _genericControllerAttributeProvider = genericControllerAttributeProvider;

            _supportedTypes = new Lazy<IEnumerable<Type>>(() => GetSupportedTypesLazy().ToList());
        }

        internal IEnumerable<Type> GetSupportedTypes()
        {
            return _supportedTypes.Value;
        }

        private IEnumerable<Type> GetSupportedTypesLazy()
        {
            // First find all types which are declared with a specific attribute
            foreach (var attribute in _genericControllerAttributeProvider.GetAll())
            {
                yield return attribute.Type;
            }
        }
    }
}