namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            FizzBuzz test = new FizzBuzz(20);
            test.Fun();
        }
    }
    class FizzBuzz
    {
        private int cap;

        public FizzBuzz(int capacity)
        {
            cap = capacity;
        }
        
        public void Fun()
        {
            for(int i = 1; i <= cap;i++)
            {
                string output = "";

                if(i % 3 == 0)
                {
                    output += "Fizz";
                }
                if(i % 5 == 0)
                {
                    output += "Buzz";
                }
                if(output == "")
                {
                    output += i.ToString();
                }
                Console.WriteLine(output);
            }
        }
    }
}
