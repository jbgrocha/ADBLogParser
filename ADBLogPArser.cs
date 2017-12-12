using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LogParser
{
    class ADBLogParser
    {
        private string filePath { get; set; }
        private List<string> fileLines { get; set; }
        private List<string[]> unparsedEvents { get; set; }

        public ADBLogParser(string filePath)
        {
            this.filePath = filePath;
            this.readFile();
            this.discardLines();
            this.cleanUpLines();
            this.readUnparsedEvents();
        }

        private void readUnparsedEvents()
        {
            unparsedEvents = new List<string[]>();

            foreach(string line in fileLines)
            {
                string[] unparsedEvent = this.readUnparsedEvent(line);
                unparsedEvents.Add(unparsedEvent);
            }
        }

        private string[] readUnparsedEvent(string line)
        {
            string[] result = line.Split(' ');
            return result;
        }

        public void printUnparsedEvents()
        {
            foreach(string[] unparsedEvent in unparsedEvents)
            {
                this.printUnparsedEvent(unparsedEvent);
            }
        }

        private void printUnparsedEvent(string[] unparsedEvent)
        {
            foreach(string property in unparsedEvent)
            {
                Console.Out.Write(property + " ");
            }
            Console.Out.WriteLine();
        }

        private void cleanUpLines()
        {
            for(int i = 0; i < fileLines.Count; i++)
            {
                fileLines[i] = this.cleanUpLine(fileLines[i]);
            }
        }

        private string cleanUpLine(string line)
        {
            string result = line;

            result = result.Replace("[", "").Replace("]", "");
            result = result.Trim();
            result = Regex.Replace(result, @"\s+", " ");

            return result;
        }

        private void discardLines()
        {
            List<string> cleanedFileLines = new List<string>();

            for (int i = 0; i < fileLines.Count; i++ )
            {
                string str = fileLines[i];

                if (this.keepLine(str))
                {
                    cleanedFileLines.Add(str);
                } 
            }

            this.fileLines = cleanedFileLines;
        }

        private bool keepLine(string str)
        {
            bool discardTest = String.IsNullOrWhiteSpace(str) || str.StartsWith("add device ") || str.StartsWith("  name:");

            bool result = !discardTest;

            return result;
        }

        public void printFileLines()
        {
            foreach(string line in fileLines)
            {
                Console.Out.WriteLine(line);
            }
        }

        private void readFile()
        {
            try
            {
                this.fileLines = new List<string>(File.ReadAllLines(filePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
