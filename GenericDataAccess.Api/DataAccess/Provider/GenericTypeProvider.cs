using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.DataAccess.Provider
{
    public interface IGenericTypeProvider
    {
        IEnumerable<Type> GetSupportedTypes();
    }

    public class GenericTypeProvider : IGenericTypeProvider
    {
        private static readonly IGenericControllerAttributeProvider GenericControllerAttributeProvider = new GenericControllerAttributeProvider();
        private static readonly Lazy<IEnumerable<Type>> SupportedTypes = new Lazy<IEnumerable<Type>>(GenericControllerAttributeProvider.GetAll().Select(a => a.Type).ToList);

        public IEnumerable<Type> GetSupportedTypes()
        {
            return SupportedTypes.Value;
        }
    }
}