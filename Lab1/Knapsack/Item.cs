using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Knapsack
{
    class Item
    {
        public int Index { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
        public double ValueToWeightRatio { get; set; }

        
        public Item(int index,int weight, int value)
        {
            Weight = weight;
            Value = value;
            Index = index;
            ValueToWeightRatio = (double)Value / (double)Weight;
        }
    }
}
