using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordsCounter
{
    class WordProcessor
    {
        public enum FileCategory : int
        {
            XS,
            S,
            M,
            L
        };

        string[] lines = null;

        public WordProcessor(string fileName)
        {
            lines = File.ReadAllLines(fileName);
        }

        public int GetWordsCount()
        {
            return lines.Length;
        }

        public int GetDictinctWordsWords()
        {
            HashSet<string> uniqueLines = new HashSet<string>(lines);
            return uniqueLines.Count;
        }

        public bool SearchForWord(string word)
        {
            return Array.IndexOf(lines, word) >= 0;
        }

        public FileCategory GetFileCategory()
        {
            FileCategory category = FileCategory.L;
            if(lines.Length >= 0 && lines.Length < 5)
            {
                category = FileCategory.XS;
            }
            else if(lines.Length >= 5 && lines.Length < 10)
            {
                category = FileCategory.S;
            }
            else if(lines.Length >= 10 && lines.Length < 15)
            {
                category = FileCategory.M;
            }
            return category;
        }

        public string[] Lines
        {
            get
            {
                return lines;
            }
        }
    }
}
