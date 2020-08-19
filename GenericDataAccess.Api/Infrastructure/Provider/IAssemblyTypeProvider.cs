using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Provider
{
    public interface IAssemblyTypeProvider
    {
        IEnumerable<Type> GetAll();
    }
}