using System;
using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.Provider;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Strategies
{
    public class CollectApiVersionFromNormalControllers : IApiVersionCollectorStrategy
    {
        private static readonly AssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();
        private static readonly Lazy<IEnumerable<ApiVersion>> ApiVersionsLazy = new Lazy<IEnumerable<ApiVersion>>(CollectInternal().ToList);

        public IEnumerable<ApiVersion> Collect()
        {
            return ApiVersionsLazy.Value;
        }

        private static IEnumerable<ApiVersion> CollectInternal()
        {
            var allApiVersions = AssemblyTypeProvider.GetAll()
                .Where(type => type.HasCustomAttribute<ApiVersionAttribute>() && type.HasCustomAttribute<ApiControllerAttribute>())
                .SelectMany(type => type.GetCustomAttributes<ApiVersionAttribute>())
                .SelectMany(attribute => attribute.Versions)
                .Distinct()
                .OrderBy(version => version.ToString());
            return allApiVersions;
        }
    }
}