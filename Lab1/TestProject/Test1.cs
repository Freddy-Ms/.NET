
using Knapsack;

namespace TestProject
{
    [TestClass]
    public sealed class KnapsackTests
    {
        [TestMethod]
        public void TestAtLeastOneItemIfPossible()
        {
            int numberOfItems = 5;
            int capacity = 6;
            List<Item> items = new List<Item>() {
                new Item(1,10, 1),
                new Item(2, 20, 2),
                new Item(3, 30, 3),
                new Item(4, 40, 4),
                new Item(5,5, 5)
            };

            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int number = result.nrOfItems();

            Assert.IsTrue(number >= 1);
        }

        [TestMethod]
        public void TestNoItemsIfCapacityZero()
        {
            int numberOfItems = 2;
            int capacity = 0;
            List<Item> items = new List<Item>()
            {
                new Item(1,5,5),
                new Item(2,10,10)
            };
            int expectedOutput = 0;

            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.nrOfItems();

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void TestCorrectSolutionForSpecificInstance()
        {
            int numberOfItems = 5;
            int capacity = 50;
            List<Item> items = new List<Item>()
            {
                new Item(1,5,5),
                new Item(2,10,10),
                new Item(3,15,15),
                new Item(4,10,15),
                new Item(5,13,15)
            };
            int expectedTotalValue = 5 + 10 + 15 + 15; // 45

            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.TotalValue;

            Assert.AreEqual(expectedTotalValue, output);
        }

        [TestMethod]
        public void TestItemsWithSameValueToWeightRatio()
        {
            int numberOfItems = 4;
            int capacity = 10;
            List<Item> items = new List<Item>()
            {
                new Item(1, 2, 4),  
                new Item(2, 4, 8),  
                new Item(3, 6, 12), 
                new Item(4, 8, 16)  
            };
            int expectedTotalValue = 12; 

            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.TotalValue;

            Assert.AreEqual(expectedTotalValue, output);
        }

        [TestMethod]
        public void TestHeavyItemsWithHighValue()
        {
            int numberOfItems = 3;
            int capacity = 14;
            List<Item> items = new List<Item>()
            {
                new Item(1, 10, 100),
                new Item(2, 15, 150),
                new Item(3, 20, 200)
            };
            int expectedTotalValue = 100; 

            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.TotalValue;

            Assert.AreEqual(expectedTotalValue, output);
        }

        [TestMethod]
        public void TestTotalWeightDoesNotExceedCapacity()
        {
            int capacity = 10;
            List<Item> items = new List<Item>()
            {
                new Item(1, 3, 6),
                new Item(2, 4, 8),
                new Item(3, 5, 10),
                new Item(4, 8, 15)
            };

            Problem problem = new Problem(items.Count, items);
            Result result = problem.Solve(capacity);

            Assert.IsTrue(result.GetTotalWeight() <= capacity);
        }

        [TestMethod]
        public void TestEmptyItemList()
        {
            int capacity = 10;
            List<Item> items = new List<Item>(); 

            Problem problem = new Problem(0, items);
            Result result = problem.Solve(capacity);

            Assert.AreEqual(0, result.TotalValue);
            Assert.AreEqual(0, result.nrOfItems());
        }

        [TestMethod]
        public void TestLargeCapacityTakesEverything()
        {
            int capacity = 100;
            List<Item> items = new List<Item>()
            {
                new Item(1, 5, 10),
                new Item(2, 10, 20),
                new Item(3, 15, 30)
            };

            Problem problem = new Problem(items.Count, items);
            Result result = problem.Solve(capacity);

            Assert.AreEqual(3, result.nrOfItems());
            Assert.AreEqual(60, result.TotalValue);
        }

        [TestMethod]
        public void TestNoItemFits()
        {
            int capacity = 4;
            List<Item> items = new List<Item>()
            {
                new Item(1, 5, 10),
                new Item(2, 6, 12),
                new Item(3, 7, 15)
            };

            Problem problem = new Problem(items.Count, items);
            Result result = problem.Solve(capacity);

            Assert.AreEqual(0, result.nrOfItems());
            Assert.AreEqual(0, result.TotalValue);
        }
        [TestMethod]
        public void TestExtremeScenario()
        {
            int numberOfItems = 10000; 
            int capacity = 5000; 

            Problem problem = new Problem(numberOfItems,500,1000,13);
            Result result = problem.Solve(capacity);

            Assert.IsTrue(result.GetTotalWeight() <= capacity);

            Assert.IsTrue(result.TotalValue > 0);

            List<Item> takenItemsInOrder = result.GetItems();
            for (int i = 1; i < takenItemsInOrder.Count; i++)
            {
                Assert.IsTrue(takenItemsInOrder[i - 1].ValueToWeightRatio >= takenItemsInOrder[i].ValueToWeightRatio);
            }
        }
    }


}
