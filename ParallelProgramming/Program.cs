using System.Diagnostics;

namespace ParallelProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSizes = { 100, 200, 500, 1000 };  
            int[] threadCounts = { 1, 2, 4, 8, 16, 32 };  
            int repetitions = 50;  

            Console.WriteLine("Rozmiar macierzy | Liczba wątków | Parallel | Threads");

            foreach (var size in matrixSizes)
            {
                Matrix a = new Matrix(size, size, randomize: true);
                Matrix b = new Matrix(size, size, randomize: true);

                foreach (var threads in threadCounts)
                {
                    double averageTimeParallel = 0;
                    double averageTimeThreads = 0;
                    for (int i = 0; i < repetitions; i++)
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        Matrix result = MatrixMultiplier.MultiplyWithParallel(a, b, threads);
                        sw.Stop();
                        averageTimeParallel += sw.ElapsedMilliseconds;

                        sw = Stopwatch.StartNew();
                        Matrix result2 = MatrixMultiplier.MultiplyWithThreads(a, b, threads);
                        sw.Stop();
                        averageTimeThreads += sw.ElapsedMilliseconds;
                    }

                    averageTimeParallel /= repetitions;
                    averageTimeThreads /= repetitions;
                    Console.WriteLine($"{size}x{size}         | {threads}           | {averageTimeParallel:F2} ms       | {averageTimeThreads:F2} ms");
                }
                Console.WriteLine();
            }
        }
    }
}
