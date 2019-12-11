using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileProcessor
{
    class Processor
    {
        static private Dictionary<string, string> sharedData = new Dictionary<string, string>();
        private string file = string.Empty;
        private static volatile bool mustStop = false;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(4);

        public Processor()
        {
            EventAgregator.Subscribe(this.Subscription);
        }

        public void Run()
        {
            Console.WriteLine("<{0}>[Processor] Enter run", Thread.CurrentThread.GetHashCode());
            while(!Processor.mustStop)
            {
                if(file != string.Empty)
                {
                    semaphore.Wait();

                    Console.WriteLine("<{1}>[Processor] New file {0}", file, Thread.CurrentThread.GetHashCode());
                  
                    string fileData = string.Empty;
                    try
                    {
                        fileData = File.ReadAllText(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("<{1}>[Processor] Run exception {0}", e.Message, Thread.CurrentThread.GetHashCode());
                        Clean();
                        continue;
                    }
                    if (fileData == string.Empty)
                    {
                        Console.WriteLine("<{1}>[Processor] No data in file {0}", file, Thread.CurrentThread.GetHashCode());
                        Clean();
                        continue;
                    }
                    Console.WriteLine("<{2}>[Processor] Size of file {0} is {1}", file, fileData.Length, Thread.CurrentThread.GetHashCode());

                    Monitor.Enter(Processor.sharedData);
                    if (Processor.sharedData.ContainsKey(file))
                    {
                        Console.WriteLine("<{1}>[Processor] File {0} exists", file, Thread.CurrentThread.GetHashCode());
                        Monitor.Exit(Processor.sharedData);
                        Clean();
                        continue;
                    }
                    Console.WriteLine("<{1}>[Processor] Add file {0}", file, Thread.CurrentThread.GetHashCode());
                    Processor.sharedData.Add(file, fileData);
                    Console.WriteLine("<{1}>[Processor] sharedData size {0}", Processor.sharedData.Count, Thread.CurrentThread.GetHashCode());
                    if (Processor.sharedData.Count >= 10)
                    {
                        foreach (var data in Processor.sharedData)
                        {
                            Console.WriteLine("<{2}>File: {0}, Data: {1}", data.Key, data.Value, Thread.CurrentThread.GetHashCode());
                        }
                        Processor.sharedData.Clear();
                    }
                    Console.WriteLine("<{1}>[Processor] Done adding file {0}", file, Thread.CurrentThread.GetHashCode());
                    Monitor.Exit(Processor.sharedData);

                    Clean();
                }

                Thread.Sleep(100);
            }
            
            Console.WriteLine("<{0}>[Processor] Exit run", Thread.CurrentThread.GetHashCode());
        }

        public void Subscription(ProcessingEvent evt)
        {
            Monitor.Enter(evt);
            if(evt.Handled)
            {
                Monitor.Exit(evt);
                return;
            }
            evt.Handled = true;
            EventAgregator.UnSubscribe(this.Subscription);
            Monitor.Exit(evt);

            file = evt.FileName;
        }

        public static void Stop()
        {
            mustStop = true;
        }

        private void Clean()
        {
            semaphore.Release();
            file = string.Empty;
            EventAgregator.Subscribe(this.Subscription);
        }
    }
}
