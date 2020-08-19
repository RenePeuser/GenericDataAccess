using Api.Infrastructure.Provider;

namespace Api.DataAccess.Provider
{
    public class GenericDbContextV1 : GenericDbContextBase
    {
        public GenericDbContextV1(IEntityProvider entityProvider) : base(entityProvider, "InMemoryDb-V1")
        {
        }
    }
}