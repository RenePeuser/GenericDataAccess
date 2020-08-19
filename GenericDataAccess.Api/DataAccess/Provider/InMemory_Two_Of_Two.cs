using Api.Infrastructure.Provider;

namespace Api.DataAccess.Provider
{
    public class InMemory_Two_Of_Two : GenericDbContext
    {
        public InMemory_Two_Of_Two(IEntityProvider entityProvider) : base(entityProvider, "InMemoryDb-V2-2")
        {
        }
    }
}