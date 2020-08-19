using System;
using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.Attributes;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Provider
{
    public class ApiVersionStaticControllerProvider : IApiVersionCollectorStrategy
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

    public class ApiVersionGenericControllerProvider : IApiVersionCollectorStrategy
    {
        private static readonly AssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();
        private static readonly IEntityProvider EntityProvider = new EntityProvider();
        private static readonly Lazy<IEnumerable<ApiVersion>> ApiVersionsLazy = new Lazy<IEnumerable<ApiVersion>>(CollectInternal().ToList);

        public IEnumerable<ApiVersion> Collect()
        {
            return ApiVersionsLazy.Value;
        }

        private static IEnumerable<ApiVersion> CollectInternal()
        {
            var genericControllerVersions = AssemblyTypeProvider.GetAll()
                .Where(type => type.HasCustomAttribute<ApiVersionAttribute>() && type.HasCustomAttribute<GenericControllerNameAttribute>())
                .SelectMany(type => type.GetCustomAttributes<ApiVersionAttribute>())
                .SelectMany(attribute => attribute.Versions)
                .Distinct()
                .OrderBy(version => version.ToString());

            var entityVersions = EntityProvider.GetAll()
                .SelectMany(type => type.GetCustomAttributes<ApiVersionAttribute>())
                .SelectMany(attribute => attribute.Versions);

            var usedVersionsAtRuntime = genericControllerVersions.Intersect(entityVersions);
            return usedVersionsAtRuntime;
        }
    }

    public interface IApiVersionCollectorStrategy
    {
        IEnumerable<ApiVersion> Collect();
    }

    public class ApiVersionProvider : IApiVersionProvider
    {
        private static readonly IEnumerable<IApiVersionCollectorStrategy> ApiVersionCollectorStrategies = new IApiVersionCollectorStrategy[]
            {new ApiVersionGenericControllerProvider(), new ApiVersionStaticControllerProvider()};

        private static readonly Lazy<IEnumerable<ApiVersion>> GetAllApiVersions = new Lazy<IEnumerable<ApiVersion>>(() => GetAllInternal().ToList());

        public IEnumerable<ApiVersion> GetAll()
        {
            return GetAllApiVersions.Value;
        }

        private static IEnumerable<ApiVersion> GetAllInternal()
        {
            return ApiVersionCollectorStrategies.SelectMany(strategy => strategy.Collect());
        }
    }
}