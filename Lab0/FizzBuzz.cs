using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class FizzBuzz
    {
        private int cap;

        public FizzBuzz(int capacity)
        {
            cap = capacity;
        }

        public void Fun()
        {
            for (int i = 1; i <= cap; i++)
            {
                string output = "";

                if (i % 3 == 0)
                {
                    output += "Fizz";
                }
                if (i % 5 == 0)
                {
                    output += "Buzz";
                }
                if (output == "")
                {
                    output += i.ToString();
                }
                Console.WriteLine(output);
            }
        }
    }
}
