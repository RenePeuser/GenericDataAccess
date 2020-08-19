using Api.Infrastructure.Provider;

namespace Api.DataAccess.Provider
{
    public class GenericDbContextV3 : GenericDbContextBase
    {
        public GenericDbContextV3(IEntityProvider entityProvider) : base(entityProvider, "InMemoryDb-V3")
        {
        }
    }
}
