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
            var randomList = GetRandomList(1000000);    
            //ThreadPool.SetMinThreads(threads, threads);
            //ThreadPool.SetMaxThreads(threads,threads);
            var watch = Stopwatch.StartNew();

            Parallel.For(0, 100, new ParallelOptions() { MaxDegreeOfParallelism = threads }, d =>
             {
                 GetLessThan10000(randomList);
             });
            //CountdownEvent e = new CountdownEvent(1);
            //for (int i = 0; i < 100; i++)
            //{
            //    e.AddCount();
            //    ThreadPool.QueueUserWorkItem(target =>
            //    {
            //        GetLessThan10000(randomList);
            //        e.Signal();
            //    });     
            //}
            //e.Signal();
            //e.Wait();
            watch.Stop();
            Console.WriteLine("Time: " + watch.ElapsedMilliseconds + "ms");

            Console.ReadLine();
        }


        public static IEnumerable<int> GetRandomList(int size)
        {
            var random = new Random();
            for (int k = 0; k < size; k++)
            {
                yield return random.Next(1, 20000);
            }
        }

        public static IEnumerable<int> GetLessThan10000(IEnumerable<int> input)
        {
            var list = new List<int>();
            foreach (var item in input)
            {
                if (item <= 10000)
                    list.Add(item);
            }
            return list;
        }
    }
}