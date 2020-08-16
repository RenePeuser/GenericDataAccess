using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Pack;
using GenericDataAccess.Models;

namespace GenericDataAccess.DataAccess.Provider
{
    public class EntityProvider
    {
        public IEnumerable<Type> GetAll()
        {
            return this.GetType().Assembly.GetTypes().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList();
        }
    }
}