using System;
using System.Collections.Generic;

namespace Api.Infrastructure.Provider
{
    public interface IEntityProvider
    {
        IEnumerable<Type> GetAll();
    }
}