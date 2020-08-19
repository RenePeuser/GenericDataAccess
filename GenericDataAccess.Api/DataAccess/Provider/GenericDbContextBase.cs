using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Api.Infrastructure.Provider;
using Extensions.Pack;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Provider
{
    public abstract class GenericDbContextBase : DbContext
    {
        private readonly IEntityProvider _entityProvider;
        private readonly string _databaseName;

        protected GenericDbContextBase(IEntityProvider entityProvider, string databaseName)
        {
            _entityProvider = entityProvider;
            _databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured.IsFalse())
            {
                optionsBuilder.UseInMemoryDatabase(_databaseName);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var types = _entityProvider.GetAll();

            foreach (var type in types)
            {
                var propertyInfos = type.GetProperties();
                var propertyNameOfKey = propertyInfos.FirstOrDefault(p => CustomAttributeProviderExtensions.HasCustomAttribute<KeyAttribute>(p));
                if (propertyNameOfKey.IsNull())
                {
                    throw new InvalidDataException($"Type: {type.Name} does not have defined key property");
                }

                modelBuilder.Entity(type, item =>
                {
                    item.HasKey(propertyNameOfKey.Name);
                    foreach (var propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.EqualsTo(propertyNameOfKey))
                        {
                            continue;
                        }

                        item.Property(propertyInfo.PropertyType, propertyInfo.Name);
                    }
                });
            }
        }
    }
}