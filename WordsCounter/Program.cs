using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WordsCounter
{
    class Program
    {
        private static List<string> allLines = new List<string>();
        private static volatile string wordToFind = "ebisewe";

        static void Main(string[] args)
        {
            string dataFolder = AppDomain.CurrentDomain.BaseDirectory;
            dataFolder = dataFolder.Substring(0, dataFolder.IndexOf(@"\bin\")) + @"\data\";
            Console.WriteLine("Data folder: " + dataFolder);


            var parent = Task.Factory.StartNew((dataFolder) =>
            {
                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None);

                for (int i = 0; i < 10; i++)
                {
                    childFactory.StartNew((fileName) => { ProcessFile(fileName); }, dataFolder + "file." + i + ".dat");
                }
            }, dataFolder);

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

            Console.WriteLine("Tasks ended.\nSummary:\n");
            Console.WriteLine("Total words: {0}", allLines.Count);
            Console.WriteLine("Searching for {0}...", wordToFind);
            Parallel.ForEach(allLines, SearchForWord);
            Console.WriteLine("Done");

        }

        private static void ProcessFile(object obj)
        {
            string fileName = Path.GetFileName(obj.ToString());

            WordProcessor processor = new WordProcessor(obj.ToString());
            int wordsCount = processor.GetWordsCount();
            Console.WriteLine("[{0}] Words count: {1}", fileName, wordsCount);
            int distinctWordsCount = processor.GetDictinctWordsWords();
            Console.WriteLine("[{0}] Distinct words count: {1}", fileName, distinctWordsCount);
            bool wordFound = processor.SearchForWord(wordToFind);
            Console.WriteLine("[{0}] Word '{1}' was {2}found", fileName, wordToFind, wordFound ? "" : "not ");
            WordProcessor.FileCategory fileCategory = processor.GetFileCategory();
            Console.WriteLine("[{0}] File category: {1}", fileName, fileCategory);

            string[] lines = processor.Lines;
            Monitor.Enter(allLines);
            allLines.AddRange(lines);
            Monitor.Exit(allLines);
        }

        private static void SearchForWord(string current, ParallelLoopState loopState, long index)
        {
            if(current == wordToFind)
            {
                Console.WriteLine("Word '{0}' found at index {1}", wordToFind, index);
                //loopState.Break();
            }
        }
    }
}
