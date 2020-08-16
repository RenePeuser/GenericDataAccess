using System.Threading.Tasks;

namespace GenericDataAccess
{
    public class Program
    {
        public static Task<int> Main()
        {
            return new AppBuilder().Build().RunAsync();
        }
    }
}
