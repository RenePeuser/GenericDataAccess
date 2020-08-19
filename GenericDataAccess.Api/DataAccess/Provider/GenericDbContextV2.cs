using Api.Infrastructure.Provider;

namespace Api.DataAccess.Provider
{
    public class GenericDbContextV2 : GenericDbContextBase
    {
        public GenericDbContextV2(IEntityProvider entityProvider) : base(entityProvider, "InMemoryDb-V2")
        {
        }
    }
}