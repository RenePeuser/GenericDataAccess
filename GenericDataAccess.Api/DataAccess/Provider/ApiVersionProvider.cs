using System;
using System.Collections.Generic;
using System.Linq;
using Api.Controllers;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.DataAccess.Provider
{
    public class ApiVersionProvider : IApiVersionProvider
    {
        private static readonly Lazy<IEnumerable<ApiVersion>> GetAllApiVersions = new Lazy<IEnumerable<ApiVersion>>(() => GetAllInternal().ToList());
        private static readonly AssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();

        public IEnumerable<ApiVersion> GetAll() => GetAllApiVersions.Value;

        private static IEnumerable<ApiVersion> GetAllInternal()
        {

            var allApiVersions = AssemblyTypeProvider.GetAll()
                                                     .Where(type => type.HasCustomAttribute<ApiVersionAttribute>() && (type.HasCustomAttribute<ApiControllerAttribute>() || type.HasCustomAttribute<GenericControllerNameAttribute>()))
                                                     .SelectMany(type => type.GetCustomAttributes<ApiVersionAttribute>())
                                                     .SelectMany(attribute => attribute.Versions)
                                                     .Distinct()
                                                     .OrderBy(version => version.ToString());
            return allApiVersions;
        }
    }
}