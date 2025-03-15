using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("TestProject")]
namespace Knapsack
{
    class Problem
    {
        private int numberOfItems;
        private int maxWeight;
        private int maxValue;
        private List<Item> items;
        //for console app
        public Problem(int numberOfItems, int maxWeight, int maxValue, int seed)
        {
            this.numberOfItems = numberOfItems;
            this.maxWeight = maxWeight;
            this.maxValue = maxValue;
            items = new List<Item>();
            this.generateItems(seed);
        }
        //for tests
        public Problem(int numberOfItems, List<Item> items)
        {
            this.numberOfItems = numberOfItems;
            this.items = items;
        }

        private void generateItems(int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < numberOfItems; i++)
            {
                items.Add(new Item((i+1),random.Next(1, maxWeight), random.Next(1, maxValue)));
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Total Items: " + numberOfItems);
            sb.AppendLine("Max Possible Weight: " + maxWeight);
            sb.AppendLine("Max Possible Value: " + maxValue);
            sb.AppendLine("Items:");
            for(int i = 0; i < this.numberOfItems; i++) 
            {
                sb.AppendLine("Nr: " + items[i].Index + " Weight: " + items[i].Weight + " Value: " + items[i].Value);
            }
            return sb.ToString();
        }
        private void SortByValueToWeightRatio()
        {
            items.Sort((x, y) => (y.ValueToWeightRatio).CompareTo(x.ValueToWeightRatio));
        }
        public Result Solve(int capacity)
        {
            SortByValueToWeightRatio();
            List<Item> takeItems = new List<Item>();
            int totalWeight = 0;
            int totalValue = 0;
            for(int i = 0; i < this.numberOfItems;i++)
            {
                if (totalWeight + items[i].Weight <= capacity)
                {
                    takeItems.Add(items[i]);
                    totalWeight += items[i].Weight;
                    totalValue += items[i].Value;
                }
            }
            return new Result(totalValue, totalWeight, takeItems);

        }
       


    }
}
