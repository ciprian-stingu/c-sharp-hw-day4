using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadedDivisors
{
    class Program
    {
        private static Dictionary<int, List<int>> results = new Dictionary<int, List<int>>();
        private static int maxNumber = 100000;
        static CountdownEvent countdown = null;

        static void Main(string[] args)
        {
            /* List<int> tmp = Divisor.Compute(100);
             foreach (int i in tmp)
             {
                 Console.Write(i + ", ");
             }
             Console.WriteLine("\n");*/

            int noOfCores = System.Environment.ProcessorCount;
            Console.WriteLine("Number of cores: {0}", noOfCores);

            //preare the lists of numbers
            List<List<int>> numbers = new List<List<int>>();
            for (int i = 0; i < noOfCores; i++)
            {
                numbers.Add(new List<int>());
            }

            //uniformly distribution
            for (int i = 1; i <= maxNumber;)
            {
                for (int j = 0; j + i <= maxNumber && j < noOfCores; j++)
                {
                    numbers[j].Add(i + j);
                }
                i += noOfCores;
            }

            Console.WriteLine("Compute using Tasks...");
            ProcessUsingTasks(numbers);
            Console.WriteLine("Compute using Threads...");
            ProcessUsingTheads(numbers);

        }

        private static void ProcessUsingTasks(List<List<int>> numbers)
        {
            results.Clear();

            var stopwatch = Stopwatch.StartNew();

            var parent = Task.Factory.StartNew((listOfListOfNumbers) =>
            {
                List<List<int>> numbers = listOfListOfNumbers as List<List<int>>;
                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None);
                foreach (var taskListOfNumbers in numbers)
                {
                    childFactory.StartNew((listOfNumbers) => { ProcessListWithTasks(listOfNumbers); }, taskListOfNumbers);
                }
            }, numbers);

            try
            {
                parent.Wait();
            }
            catch (AggregateException aex)
            {
                aex.Flatten().Handle(ex =>
                {
                    Console.WriteLine(ex.Message + " " + ex.StackTrace);
                    return true;
                });
            }
            stopwatch.Stop();

            var result = results.OrderByDescending(a => a.Value.Count).Select(a => a).First();
            Console.WriteLine("Number: " + result.Key + ", Divisors: " + result.Value.Count);
            Console.WriteLine("Time: {0:0} ms", stopwatch.Elapsed.TotalMilliseconds);
        }

        private static void ProcessListWithTasks(object obj)
        {
            List<int> no = obj as List<int>;

            foreach (int i in no)
            {
                List<int> divisors = Divisor.Compute(i);
                Monitor.Enter(results);
                results.Add(i, divisors);
                Monitor.Exit(results);
            }
        }

        private static void ProcessUsingTheads(List<List<int>> numbers)
        {
            results.Clear();

            var stopwatch = Stopwatch.StartNew();
            countdown = new CountdownEvent(numbers.Count);

            foreach(List<int> listOfListOfNumbers in numbers)
            {
                new Thread(ProcessListWithThreads).Start(listOfListOfNumbers);
            }

            countdown.Wait();
            stopwatch.Stop();

            var result = results.OrderByDescending(a => a.Value.Count).Select(a => a).First();
            Console.WriteLine("Number: " + result.Key + ", Divisors: " + result.Value.Count);
            Console.WriteLine("Time: {0:0} ms", stopwatch.Elapsed.TotalMilliseconds);
        }

        private static void ProcessListWithThreads(object obj)
        {
            List<int> no = obj as List<int>;

            foreach (int i in no)
            {
                List<int> divisors = Divisor.Compute(i);
                Monitor.Enter(results);
                results.Add(i, divisors);
                Monitor.Exit(results);
            }
            countdown.Signal();
        }
    }
}
