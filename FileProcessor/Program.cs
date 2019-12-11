using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFolder = AppDomain.CurrentDomain.BaseDirectory;
            dataFolder = dataFolder.Substring(0, dataFolder.IndexOf(@"\bin\")) + @"\data\";
            Console.WriteLine("Data folder: " + dataFolder);

            Watcher watcher = new Watcher(dataFolder);
            Thread watcherThread = new Thread(RunWatcher);
            watcherThread.Start(watcher);

            Thread aggregatorThread = new Thread(RunAggregator);
            aggregatorThread.Start();

            var parent = Task.Factory.StartNew(() =>
            {
                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None);
                for (int i = 0; i < 10; i++)
                {
                    childFactory.StartNew(() => { RunProcessor(); });
                }
            });

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();

            EventAgregator.WaitHandle.Set();
            Processor.Stop();
            parent.Wait();
            watcher.WaitHandle.Set();
        }

        private static void RunWatcher(object obj)
        {
            Watcher watcher = obj as Watcher;
            if(watcher != null) {
                watcher.Run();
            }
        }

        private static void RunAggregator()
        {
            EventAgregator.Run();
        }

        private static void RunProcessor()
        {
            Processor processor = new Processor();
            processor.Run();
        }
    }
}
