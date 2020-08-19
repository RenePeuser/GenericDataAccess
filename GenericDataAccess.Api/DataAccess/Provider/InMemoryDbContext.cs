using Api.Infrastructure.Provider;

namespace Api.DataAccess.Provider
{
    public class InMemoryDbContext : GenericDbContext
    {
        public InMemoryDbContext(IEntityProvider entityProvider) : base(entityProvider, "InMemoryDb-V1")
        {
        }
    }
}
