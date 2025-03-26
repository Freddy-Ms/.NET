using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormTest")]
namespace API
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            APITest A = new APITest();
            A.GetData().Wait();
        }
    }
}
