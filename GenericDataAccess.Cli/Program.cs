using System.Threading.Tasks;

namespace Cli
{
    public class Program
    {
        public static Task<int> Main()
        {
            return new AppBuilder().Build().RunAsync();
        }
    }
}