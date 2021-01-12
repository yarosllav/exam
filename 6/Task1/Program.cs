using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int.TryParse(Console.ReadLine(), out var threads);
            ThreadPool.SetMinThreads(threads, threads);
            ThreadPool.SetMaxThreads(threads, threads);
            var watch = Stopwatch.StartNew();
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = new Task(Count);
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            watch.Stop();

            Console.WriteLine("Time: " + watch.ElapsedMilliseconds + "ms");
            Console.ReadLine();
        }


        public static void Count()
        {
            for (int i = 1; i <= 10000; i++)
            {
                IsPrimeNumber(i);
            }
        }

        public static bool IsPrimeNumber(int n)
        {
            var result = true;
            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}