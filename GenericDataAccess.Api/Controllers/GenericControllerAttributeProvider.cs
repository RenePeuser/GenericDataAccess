using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extensions.Pack;

namespace Api.Controllers
{
    public class GenericControllerAttributeProvider : IGenericControllerAttributeProvider
    {
        private static readonly Lazy<IEnumerable<GenerateControllerInfo>> Lazy = new Lazy<IEnumerable<GenerateControllerInfo>>(CollectGenerateControllerInfoInternal);

        public IEnumerable<GenerateControllerInfo> GetAll()
        {
            return Lazy.Value;
        }

        private static IEnumerable<GenerateControllerInfo> CollectGenerateControllerInfoInternal()
        {
            var assemblies = GetAssembliesToLookFor().ToList();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes().Where(t => t.HasCustomAttribute<GenericControllerAttribute>())).ToList();
            return types.Select(t => new GenerateControllerInfo(t, t.GetCustomAttribute<GenericControllerAttribute>()));
        }

        private static IEnumerable<Assembly> GetAssembliesToLookFor()
        {
            yield return typeof(GenericControllerAttributeProvider).Assembly;
        }
    }
}