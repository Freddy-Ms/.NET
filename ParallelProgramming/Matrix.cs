using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Matrix
    {
        public int Rows { get; }
        public int Cols { get; }
        public double[,] Data { get; }

        public Matrix(int rows, int cols, bool randomize = false)
        {
            Rows = rows;
            Cols = cols;
            Data = new double[rows, cols];
            if (randomize)
            {
                Random rand = new Random();
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        Data[i, j] = rand.Next(1, 10);
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write(Data[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
