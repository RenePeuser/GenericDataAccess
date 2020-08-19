using System;
using System.Collections.Generic;
using System.Linq;
using Api.DataAccess.Models;
using Api.Infrastructure.Attributes;
using Api.Infrastructure.Models;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Provider
{
    public class GenericControllerAttributeProvider : IGenericControllerAttributeProvider
    {
        private static readonly AssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();
        private static readonly Lazy<IEnumerable<GenerateControllerInfo>> Lazy = new Lazy<IEnumerable<GenerateControllerInfo>>(CollectGenerateControllerInfoInternal);

        public IEnumerable<GenerateControllerInfo> GetAll()
        {
            return Lazy.Value;
        }

        private static IEnumerable<GenerateControllerInfo> CollectGenerateControllerInfoInternal()
        {
            var types = AssemblyTypeProvider.GetAll().Where(t => t.HasCustomAttribute<GenericControllerAttribute>());
            return types.Select(t =>
                new GenerateControllerInfo(t, t.GetCustomAttribute<GenericControllerAttribute>(), t.GetCustomAttributes<ApiVersionAttribute>().SelectMany(a => a.Versions).ToList()));
        }
    }
}