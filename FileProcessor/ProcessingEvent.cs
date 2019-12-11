using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessor
{
    class ProcessingEvent
    {
        public string FileName { get; set; }
        public bool Handled { get; set; }
    }
}
