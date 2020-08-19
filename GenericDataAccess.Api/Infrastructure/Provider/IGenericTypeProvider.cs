using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Provider
{
    public interface IGenericTypeProvider
    {
        IEnumerable<Type> GetSupportedTypes();
    }
}