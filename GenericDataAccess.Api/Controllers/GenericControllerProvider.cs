using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    internal class GenericControllerProvider
    {
        private readonly GenericTypeProvider _genericTypeProvider;

        internal GenericControllerProvider() : this(new GenericTypeProvider())
        {
        }

        internal GenericControllerProvider(GenericTypeProvider genericTypeProvider)
        {
            _genericTypeProvider = genericTypeProvider;
        }

        internal IEnumerable<Type> GetAllGenericControllerTypes()
        {
            var types = _genericTypeProvider.GetSupportedTypes().ToList();
            foreach (var type in types)
            {
                // ToDo: Next version also find all generic controllers !!
                yield return typeof(GenericController<>).MakeGenericType(type);
            }
        }
    }
}