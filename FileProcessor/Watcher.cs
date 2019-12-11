using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileProcessor
{
    class Watcher
    {
        private string path;
        private EventWaitHandle waitHandle = null;

        public Watcher(string path)
        {
            this.path = path;
            waitHandle = new ManualResetEvent(false);
        }

        public void Run()
        {
            Console.WriteLine("[Watcher] Enter run");

            FileSystemWatcher watcher = new FileSystemWatcher(path);
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(EventAgregator.Publish);
            //watcher.Created += new FileSystemEventHandler(EventAgregator.Publish);
            watcher.EnableRaisingEvents = true;

            waitHandle.WaitOne();
            Console.WriteLine("[Watcher] Exit run");
        }

        public EventWaitHandle WaitHandle
        {
            get
            {
                return waitHandle;
            }
        }
    }
}
