using System;
using System.Collections.Generic;
using Cli.Models;

namespace Cli
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