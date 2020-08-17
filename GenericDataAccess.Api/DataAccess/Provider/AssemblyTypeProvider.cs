using System;
using System.Collections.Generic;

namespace Api.DataAccess.Provider
{
    public class AssemblyTypeProvider : IAssemblyTypeProvider
    {
        // static info but non static class for possible switchable provider.
        private static readonly Lazy<IEnumerable<Type>> LazyTypes = new Lazy<IEnumerable<Type>>(() => typeof(AssemblyTypeProvider).Assembly.GetTypes());

        public IEnumerable<Type> GetAll() => LazyTypes.Value;
    }
}