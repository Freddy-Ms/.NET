
using Knapsack;

namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int numberOfItems = 5;
            int capacity = 6;
            List<Items> items = new List<Items>() {
                new Items(10, 1),
                new Items(20, 2), 
                new Items(30, 3),
                new Items(40, 4),
                new Items(5, 5)
            };
            
            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int number = result.nrOfItems();
            Assert.AreEqual(1,number);

        }
        [TestMethod]
        public void TestMethod2()
        {
            int numberOfItems = 2;
            int capacity = 0;
            List<Items> items = new List<Items>()
            {
                new Items(5,5),
                new Items(10,10)
            };
            int desireOutput = 0;
            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.nrOfItems();
            Assert.AreEqual(desireOutput, output);
        }
        [TestMethod]
        public void TestMethod3()
        {
            int numberOfItems = 5;
            int capacity = 50;
            List<Items> items = new List<Items>()
            {
                new Items(5,5),
                new Items(10,10),
                new Items(15,15),
                new Items(10,15),
                new Items(13,15)
            };
            int desireOutput = 5+10+15+15;
            Problem problem = new Problem(numberOfItems, items);
            Result result = problem.Solve(capacity);
            int output = result.TotalValue;
            Assert.AreEqual(desireOutput, output);
        }
        
    }
    
    }
