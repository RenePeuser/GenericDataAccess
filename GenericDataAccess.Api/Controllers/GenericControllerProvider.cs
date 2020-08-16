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
                yield return typeof(GenericController<>).MakeGenericType(type);
            }
        }
    }
}