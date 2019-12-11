using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileProcessor
{
    static class EventAgregator
    {
        static private BlockingCollection<string> files = new BlockingCollection<string>(100);
        public delegate void Subscription(ProcessingEvent e);
        static private Subscription subscriptions = null;
        static private EventWaitHandle waitHandle = new ManualResetEvent(false);

        //need to expose subscription using a method
        //because using a get/set-er will block the app ¯\_(ツ)_/¯
        public static void Subscribe(Subscription subscription)
        {
            subscriptions += subscription;
            Console.WriteLine("[EventAgregator] Add subscription");
        }

        public static void UnSubscribe(Subscription subscription)
        {
            subscriptions -= subscription;
            Console.WriteLine("[EventAgregator] Remove subscription");
        }

        public static void Publish(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"[EventAgregator] Publish file {e.FullPath}");
            files.Add(e.FullPath);
        }

        public static void Run()
        {
            Console.WriteLine("[EventAgregator] Enter run");
            while (true)
            {
                if (subscriptions != null && files.Count > 0)
                {
                    string file = string.Empty;

                    try
                    {
                        file = EventAgregator.files.Take();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[EventAgregator] Run exception {0}", e.Message);
                        continue;
                    }

                    ProcessingEvent evt = new ProcessingEvent();
                    evt.FileName = file;
                    subscriptions(evt);
                    
                }

                if(waitHandle.WaitOne(50))
                {
                    break;
                }
            }
            Console.WriteLine("[EventAgregator] Exit run");
        }

        static public EventWaitHandle WaitHandle
        {
            get
            {
                return waitHandle;
            }
        }
    }
}
