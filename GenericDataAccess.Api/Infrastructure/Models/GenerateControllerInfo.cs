using System;
using System.Collections.Generic;
using Api.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Models
{
    public class GenerateControllerInfo
    {
        public GenerateControllerInfo(Type type, GenericControllerAttribute genericControllerAttribute, IEnumerable<ApiVersion> apiVersions)
        {
            Type = type;
            GenericControllerAttribute = genericControllerAttribute;
            ApiVersions = apiVersions;
        }

        public Type Type { get; }

        public GenericControllerAttribute GenericControllerAttribute { get; }

        public IEnumerable<ApiVersion> ApiVersions { get; }
    }
}