﻿using ADBLogParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LogParser
{
    class ADBLogParser
    {
        private string FilePath { get; set; }
        private List<string> FileLines { get; set; }
        private List<string[]> UnparsedEvents { get; set; }
        private List<ADBLogEvent> ParsedEvents { get; set; }

        public ADBLogParser(string filePath)
        {
            this.FilePath = filePath;
            this.ReadFile();
            this.DiscardLines();
            this.CleanUpLines();
            this.ReadUnparsedEvents();
            this.ParseLogEvents();
        }

        private void ParseLogEvents()
        {
            ParsedEvents = new List<ADBLogEvent>();

            foreach(string[] unparsedEvent in UnparsedEvents)
            {
                ADBLogEvent parsedEvent = new ADBLogEvent(unparsedEvent);
                ParsedEvents.Add(parsedEvent);
            }
        }

        public void PrintParsedEvents()
        {
            foreach(ADBLogEvent parsedEvent in ParsedEvents)
            {
                Console.Out.WriteLine(parsedEvent.ToString());
            }
        }

        private void ReadUnparsedEvents()
        {
            UnparsedEvents = new List<string[]>();

            foreach(string line in FileLines)
            {
                string[] unparsedEvent = this.ReadUnparsedEvent(line);
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
            foreach(string[] unparsedEvent in UnparsedEvents)
            {
                this.PrintUnparsedEvent(unparsedEvent);
            }
        }

        private void PrintUnparsedEvent(string[] unparsedEvent)
        {
            foreach(string property in unparsedEvent)
            {
                Console.Out.Write(property + " ");
            }
            Console.Out.WriteLine();
        }

        private void CleanUpLines()
        {
            for(int i = 0; i < FileLines.Count; i++)
            {
                FileLines[i] = this.cleanUpLine(FileLines[i]);
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

        private void DiscardLines()
        {
            List<string> cleanedFileLines = new List<string>();

            for (int i = 0; i < FileLines.Count; i++ )
            {
                string str = FileLines[i];

                if (this.KeepLine(str))
                {
                    cleanedFileLines.Add(str);
                } 
            }

            this.FileLines = cleanedFileLines;
        }

        private bool KeepLine(string str)
        {
            bool discardTest = String.IsNullOrWhiteSpace(str) || str.StartsWith("add device ") || str.StartsWith("  name:");

            bool result = !discardTest;

            return result;
        }

        public void PrintFileLines()
        {
            foreach(string line in FileLines)
            {
                Console.Out.WriteLine(line);
            }
        }

        private void ReadFile()
        {
            try
            {
                this.FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
