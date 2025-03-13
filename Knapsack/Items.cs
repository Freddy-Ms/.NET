using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack
{
    class Items
    {
        public int Weight { get; set; }
        public int Value { get; set; }
        public Items(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }
    }
}
