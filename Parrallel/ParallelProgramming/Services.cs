using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class MatrixMultiplier
    {
        public static Matrix MultiplyWithParallel(Matrix a, Matrix b, int threadCount)
        {
            if (a.Cols != b.Rows)
                throw new InvalidOperationException("Incompatible matrix sizes.");

            Matrix result = new Matrix(a.Rows, b.Cols);
            int totalCells = a.Rows * b.Cols;

            Parallel.For(0, totalCells, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, index =>
            {
                int row = index / b.Cols;
                int col = index % b.Cols;
                double sum = 0;
                for (int k = 0; k < a.Cols; k++)
                    sum += a.Data[row, k] * b.Data[k, col];
                result.Data[row, col] = sum;
            });

            return result;
        }
        public static Matrix MultiplyWithThreads(Matrix a, Matrix b, int threadCount)
        {
            if (a.Cols != b.Rows)
                throw new InvalidOperationException("Incompatible matrix sizes.");

            Matrix result = new Matrix(a.Rows, b.Cols);
            int totalCells = a.Rows * b.Cols;
            int cellsPerThread = (int)Math.Ceiling((double)totalCells / threadCount); 

            Thread[] threads = new Thread[threadCount];

            void CalculateCell(int startIndex, int endIndex)
            {
                for (int index = startIndex; index < endIndex; index++)
                {
                    int row = index / b.Cols;
                    int col = index % b.Cols;
                    double sum = 0;

                    for (int k = 0; k < a.Cols; k++)
                        sum += a.Data[row, k] * b.Data[k, col];

                    result.Data[row, col] = sum;
                }
            }

            for (int i = 0; i < threadCount; i++)
            {
                int startIndex = i * cellsPerThread;
                int endIndex = Math.Min(startIndex + cellsPerThread, totalCells);
                threads[i] = new Thread(() => CalculateCell(startIndex, endIndex));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }
    }
}
