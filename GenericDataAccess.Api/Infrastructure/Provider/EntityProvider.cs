using System;
using System.Collections.Generic;
using System.Linq;
using Api.DataAccess.Models;
using Extensions.Pack;

namespace Api.Infrastructure.Provider
{
    public class EntityProvider : IEntityProvider
    {
        private static readonly Lazy<IEnumerable<Type>> LazyTypes = new Lazy<IEnumerable<Type>>(() => new AssemblyTypeProvider().GetAll().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList());

        public IEnumerable<Type> GetAll()
        {
            return LazyTypes.Value;
        }
    }
}