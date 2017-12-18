using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Strokes;

namespace ADBLogParser
{
    class EventParser
    {
        private string FilePath { get; set; }
        private List<string> FileLines { get; set; }
        private List<string[]> UnparsedEvents { get; set; }
        public List<ADBLogEvent> ParsedEvents { get; set; }

        public EventParser(string filePath)
        {
            FilePath = filePath;
            ReadFile();
            DiscardLines();
            CleanUpLines();
            ReadUnparsedEvents();
            ParseLogEvents();

        }

        private void ParseLogEvents()
        {
            ParsedEvents = new List<ADBLogEvent>();

            foreach (string[] unparsedEvent in UnparsedEvents)
            {
                ADBLogEvent parsedEvent = new ADBLogEvent(unparsedEvent);
                ParsedEvents.Add(parsedEvent);
            }
        }

        private void ReadUnparsedEvents()
        {
            UnparsedEvents = new List<string[]>();

            foreach (string line in FileLines)
            {
                string[] unparsedEvent = ReadUnparsedEvent(line);
                UnparsedEvents.Add(unparsedEvent);
            }
        }

        private string[] ReadUnparsedEvent(string line)
        {
            string[] result = line.Split(' ');
            return result;
        }

        public void PrintUnparsedEvents()
        {
            foreach (string[] unparsedEvent in UnparsedEvents)
            {
                PrintUnparsedEvent(unparsedEvent);
            }
        }

        private void PrintUnparsedEvent(string[] unparsedEvent)
        {
            foreach (string property in unparsedEvent)
            {
                Console.Out.Write(property + " ");
            }
            Console.Out.WriteLine();
        }

        private void CleanUpLines()
        {
            for (int i = 0; i < FileLines.Count; i++)
            {
                FileLines[i] = CleanUpLine(FileLines[i]);
            }
        }

        private string CleanUpLine(string line)
        {
            string result = line;

            result = result.Replace("[", "").Replace("]", "");
            result = result.Trim();
            result = Regex.Replace(result, @"\s+", " ");

            return result;
        }

        private void DiscardLines()
        {
            List<string> cleanedFileLines = new List<string>();

            for (int i = 0; i < FileLines.Count; i++)
            {
                string str = FileLines[i];

                if (KeepLine(str))
                {
                    cleanedFileLines.Add(str);
                }
            }

            FileLines = cleanedFileLines;
        }

        private bool KeepLine(string str)
        {
            bool result = str.StartsWith("[");

            return result;
        }

        public void PrintFileLines()
        {
            foreach (string line in FileLines)
            {
                Console.Out.WriteLine(line);
            }
        }

        private void ReadFile()
        {
            try
            {
                FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
