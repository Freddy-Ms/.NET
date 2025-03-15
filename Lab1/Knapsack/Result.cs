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
        private List<Item> takeItems;
        public int TotalValue { get { return totalValue; } }
        public Result(int totalValue, int totalWeight, List<Item> takeItems)
        {
            this.totalValue = totalValue;
            this.totalWeight = totalWeight;
            this.takeItems = takeItems;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Total Value: " + totalValue);
            sb.AppendLine("Total Weight: " + totalWeight);
            sb.AppendLine("Take Items:");
            for (int i = 0; i < takeItems.Count; i++)
            {
                sb.AppendLine("Nr: " + takeItems[i].Index);
            }
            return sb.ToString();
        }
        public int nrOfItems()
        {
            return takeItems.Count;
        }

        public int GetTotalWeight()
        {
            return totalWeight;
        }
        public List<Item> GetItems()
        {
            return takeItems;
        }
    }
}
