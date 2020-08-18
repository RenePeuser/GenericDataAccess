﻿using System;
using System.Collections.Generic;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models
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