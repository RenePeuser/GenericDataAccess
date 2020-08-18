using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Extensions.Pack;

namespace Api.DataAccess.Provider
{
    public interface IEntityProvider
    {
        IEnumerable<Type> GetAll();
    }

    public class EntityProvider : IEntityProvider
    {
        private static readonly Lazy<IEnumerable<Type>> LazyTypes = new Lazy<IEnumerable<Type>>(() => new AssemblyTypeProvider().GetAll().Where(t => t.HasCustomAttribute<EntityAttribute>()).ToList());

        public IEnumerable<Type> GetAll()
        {
            return LazyTypes.Value;
        }
    }
}