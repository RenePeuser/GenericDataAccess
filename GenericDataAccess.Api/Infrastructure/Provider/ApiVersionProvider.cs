using System;
using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.Strategies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Provider
{
    public class ApiVersionProvider : IApiVersionProvider
    {
        private static readonly IEnumerable<IApiVersionCollectorStrategy> ApiVersionCollectorStrategies = new IApiVersionCollectorStrategy[]
            {new CollectAllApiVersionsFromGenericController(), new CollectApiVersionFromNormalControllers()};

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