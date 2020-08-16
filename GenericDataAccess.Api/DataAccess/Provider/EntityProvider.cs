using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Extensions.Pack;

namespace Api.DataAccess.Provider
{
    public class EntityProvider
    {
        public IEnumerable<Type> GetAll()
        {
            return GetType().Assembly.GetTypes().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList();
        }
    }
}