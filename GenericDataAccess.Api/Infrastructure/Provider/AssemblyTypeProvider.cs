using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Provider
{
    public class AssemblyTypeProvider : IAssemblyTypeProvider
    {
        // This is only static because when we need that class service collection is not already build, so we can not consume services by the service collection
        private static readonly Lazy<IEnumerable<Type>> LazyTypes = new Lazy<IEnumerable<Type>>(() => typeof(AssemblyTypeProvider).Assembly.GetTypes());

        public IEnumerable<Type> GetAll()
        {
            return LazyTypes.Value;
        }
    }
}