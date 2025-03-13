using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack
{
    class Result
    {
        private int totalValue;
        private int totalWeight;
        private int[] takeItems;
        public int TotalValue { get { return totalValue; } }
        public int TotalWeight { get { return totalWeight; } }
        public int[] TakeItems { get { return takeItems; } }
        public Result(int totalValue, int totalWeight, List<int> takeItems)
        {
            this.totalValue = totalValue;
            this.totalWeight = totalWeight;
            this.takeItems = takeItems.ToArray();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Total Value: " + totalValue);
            sb.AppendLine("Total Weight: " + totalWeight);
            sb.AppendLine("Take Items:");
            for (int i = 0; i < takeItems.Length; i++)
            {
                sb.AppendLine("Nr: " + takeItems[i]);
            }
            return sb.ToString();
        }
        public int nrOfItems()
        {
            return takeItems.Length;
        }
    }
}
