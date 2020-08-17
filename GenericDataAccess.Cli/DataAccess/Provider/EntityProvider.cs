using System;
using System.Collections.Generic;
using System.Linq;
using Cli.Models;
using Extensions.Pack;

namespace Cli.DataAccess.Provider
{
    public class EntityProvider
    {
        private readonly AssemblyTypeProvider _assemblyTypeProvider;

        public EntityProvider(AssemblyTypeProvider assemblyTypeProvider)
        {
            _assemblyTypeProvider = assemblyTypeProvider;
        }

        public IEnumerable<Type> GetAll()
        {
            return _assemblyTypeProvider.GetAll().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList();
        }
    }
}