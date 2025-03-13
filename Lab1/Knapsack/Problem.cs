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
        private int totalItems;
        private int maxWeight;
        private int maxValue;
        private List<Items> items;
        //for console app
        public Problem(int totalItems, int maxWeight, int maxValue, int seed)
        {
            this.totalItems = totalItems;
            this.maxWeight = maxWeight;
            this.maxValue = maxValue;
            items = new List<Items>();
            this.generateItems(seed);
        }
        //for tests
        public Problem(int totalItems, List<Items> items)
        {
            this.totalItems = totalItems;
            this.items = items;
        }

        public int TotalItems { get { return totalItems; } }
        public int MaxWeight { get { return maxWeight; } }
        public List<Items> Items { get { return items; } }

        private void generateItems(int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < totalItems; i++)
            {
                items.Add(new Items(random.Next(1, maxWeight), random.Next(1, maxValue)));
            }
            SortByValueToWeightRatio();

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Total Items: " + totalItems);
            sb.AppendLine("Max Possible Weight: " + maxWeight);
            sb.AppendLine("Max Possible Value: " + maxValue);
            sb.AppendLine("Items:");
            for(int i = 0; i < this.totalItems; i++) 
            {
                sb.AppendLine("Nr: " + (i+1) + " Weight: " + items[i].Weight + " Value: " + items[i].Value);
            }
            return sb.ToString();
        }
        private void SortByValueToWeightRatio()
        {
            items.Sort((x, y) => (y.Value / (double)y.Weight).CompareTo(x.Value / (double)x.Weight));
        }
        public Result Solve(int capacity)
        {
            
            List<int> takeItems = new List<int>();
            int totalWeight = 0;
            int totalValue = 0;
            for(int i = 0; i < this.totalItems;i++)
            {
                if (totalWeight + items[i].Weight <= capacity)
                {
                    takeItems.Add(i+1);
                    totalWeight += items[i].Weight;
                    totalValue += items[i].Value;
                }
            }
            return new Result(totalValue, totalWeight, takeItems);

        }

            

    }
}
