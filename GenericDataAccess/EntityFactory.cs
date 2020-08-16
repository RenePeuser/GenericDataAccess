using System;
using System.Collections.Generic;
using GenericDataAccess.Models;

namespace GenericDataAccess
{
    internal class EntityFactory
    {
        public IEnumerable<TEntity> Create<TEntity>() where TEntity : EntityBase, new()
        {
            while (true)
            {
                yield return Activator.CreateInstance<TEntity>();
            }
        }
    }
}