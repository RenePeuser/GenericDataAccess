using System;
using System.Collections.Generic;

namespace Api.DataAccess.Provider
{
    public interface IAssemblyTypeProvider
    {
        IEnumerable<Type> GetAll();
    }
}