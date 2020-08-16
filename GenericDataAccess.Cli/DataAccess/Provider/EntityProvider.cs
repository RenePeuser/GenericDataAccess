using System;
using System.Collections.Generic;
using System.Linq;
using Cli.Models;
using Extensions.Pack;

namespace Cli.DataAccess.Provider
{
    public class EntityProvider
    {
        public IEnumerable<Type> GetAll()
        {
            return GetType().Assembly.GetTypes().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList();
        }
    }
}