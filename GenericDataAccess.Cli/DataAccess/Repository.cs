﻿using System.Collections.Generic;
using Cli.DataAccess.Provider;
using Cli.Models;

namespace Cli.DataAccess
{
    internal class Repository<TEntity> where TEntity : EntityBase
    {
        private readonly MyDbContext _myDbContext;

        public Repository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _myDbContext.Set<TEntity>();
        }

        public void Add(params TEntity[] entities)
        {
            _myDbContext.Set<TEntity>().AddRange(entities);
            _myDbContext.SaveChanges();
        }

        public void Delete(params TEntity[] entities)
        {
            _myDbContext.Set<TEntity>().RemoveRange(entities);
            _myDbContext.SaveChanges();
        }
    }
}