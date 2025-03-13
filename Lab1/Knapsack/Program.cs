namespace Knapsack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of items: ");
            int numberOfItems = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the possible maximum weight of a single item:");
            int maxWeight = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the possible maximum value of a single item:");
            int maxValue = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the seed: ");
            int seed = int.Parse(Console.ReadLine());
            Problem problem = new Problem(numberOfItems, maxWeight, maxValue, seed);
            Console.WriteLine(problem.ToString());
            Console.WriteLine("Enter the capacity of the Knapsnack:");
            int capacity = int.Parse(Console.ReadLine());
            Result result = problem.Solve(capacity);
            Console.WriteLine(result.ToString());


        }
    }
}
