using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.DataAccess.Provider
{
    public class ApiVersionProvider : IApiVersionProvider
    {
        private static readonly Lazy<IEnumerable<ApiVersion>> GetAllApiVersions = new Lazy<IEnumerable<ApiVersion>>(() => GetAllInternal().ToList());
       
        public IEnumerable<ApiVersion> GetAll() => GetAllApiVersions.Value;

        private static IEnumerable<ApiVersion> GetAllInternal()
        {
            return new GenericControllerProvider().GetAll()
                                                  .Select(item => item.GetCustomAttribute<ApiVersionAttribute>())
                                                  .FilterNullObjects()
                                                  .SelectMany(attribute => attribute.Versions)
                                                  .Distinct()
                                                  .ToList();
        }
    }
}