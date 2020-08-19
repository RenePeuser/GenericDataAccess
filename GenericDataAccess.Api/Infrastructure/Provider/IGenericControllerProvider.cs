using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Provider
{
    public interface IGenericControllerProvider
    {
        IEnumerable<Type> GetAll();
    }
}