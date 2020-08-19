using System;
using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.Attributes;
using Api.Infrastructure.Errorhandling;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Provider
{
    public class GenericControllerProvider : IGenericControllerProvider
    {
        private static readonly IGenericTypeProvider GenericTypeProvider = new GenericTypeProvider();
        private static readonly IAssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();
        private static readonly Lazy<IEnumerable<Type>> LazyControllerTypes = new Lazy<IEnumerable<Type>>(GetAllInternal().ToList);

        public IEnumerable<Type> GetAll()
        {
            return LazyControllerTypes.Value;
        }

        private static IEnumerable<Type> GetAllInternal()
        {
            var types = GenericTypeProvider.GetSupportedTypes().ToList();
            var allControllers = GetAllGenericControllerTypesInternal().ToList();


            foreach (var type in types)
            {
                var supportedVersion = type.GetCustomAttributes<ApiVersionAttribute>().SelectMany(attribute => attribute.Versions);
                if (supportedVersion.IsNullOrEmpty())
                {
                    throw new ProblemDetailsException(500, $"Entity type: '{type.Name}' must have version attribute to map to correct controller.",
                        $"Entity type: '{type.Name}' must have version attribute to map to correct controller.");
                }

                foreach (var genericControllerType in allControllers)
                {
                    var controllerApiVersions = genericControllerType.GetCustomAttributes<ApiVersionAttribute>().SelectMany(a => a.Versions);
                    if (controllerApiVersions.Intersect(supportedVersion).Any())
                    {
                        if (genericControllerType.IsGenericType.IsFalse())
                        {
                            throw new ProblemDetailsException(500, $"Controller which has not a generic type has an attribute: '{nameof(GenericControllerNameAttribute)}'",
                                $"Only generic controllers are allowed to add the attribute: '{nameof(GenericControllerNameAttribute)}' to generate correct controller with correct data types.");
                        }

                        yield return genericControllerType.MakeGenericType(type);
                    }
                }
            }
        }

        private static IEnumerable<Type> GetAllGenericControllerTypesInternal()
        {
            var allGenericControllers = AssemblyTypeProvider.GetAll().Where(type => type.HasCustomAttribute<GenericControllerNameAttribute>());
            return allGenericControllers;
        }
    }
}