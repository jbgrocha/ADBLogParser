﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


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

        private void CalculateTouchEventsSummary(Dictionary<string, int> TouchSummary)
        {
            foreach (ADBLogEvent logEvent in ParsedEvents)
            {
                AddTouchEventToSummary(logEvent, TouchSummary);
            }
        }

        private void AddTouchEventToSummary(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            if ((logEvent.OpCode == "EV_SYN") || (logEvent.EventType == "ABS_MT_TRACKING_ID"))
            {
                AddTouchEvent(logEvent, TouchSummary);
            }
        }

        private void AddTouchEvent(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            string key = logEvent.EventType + " " + logEvent.EventValue;

            if (!TouchSummary.ContainsKey(key))
            {
                TouchSummary.Add(key, 1);
            }
            else
            {
                TouchSummary[key] += 1;
            }
        }

        public void PrintTouchEventSummary()
        {
            Dictionary<string, int> TouchSummary = new Dictionary<string, int>();

            CalculateTouchEventsSummary(TouchSummary);

            foreach (KeyValuePair<string, int> existingMultiTouch in TouchSummary)
            {
                Console.Out.WriteLine(existingMultiTouch.Key + ": " + existingMultiTouch.Value);
            }
        }

        private void CalculateFeatureEventSummary(Dictionary<string, int> FeatureSummary)
        {
            foreach (ADBLogEvent logEvent in ParsedEvents)
            {
                AddFeatureEventToSummary(logEvent, FeatureSummary);
            }
        }

        private void AddFeatureEventToSummary(ADBLogEvent logEvent, Dictionary<string, int> FeatureSummary)
        {
            if (logEvent.OpCode == "EV_ABS")
            {
                string key = logEvent.EventType;

                if (!FeatureSummary.ContainsKey(key))
                {
                    FeatureSummary.Add(key, 1);
                }
                else
                {
                    FeatureSummary[key] += 1;
                }
            }
        }

        public void PrintFeatureEventSummary()
        {
            Dictionary<string, int> FeatureSummary = new Dictionary<string, int>();

            CalculateFeatureEventSummary(FeatureSummary);

            foreach (KeyValuePair<string, int> existingFeature in FeatureSummary)
            {
                Console.Out.WriteLine(existingFeature.Key + " " + existingFeature.Value);
            }
        }

        public void PrintParsedEvents()
        {
            foreach (ADBLogEvent parsedEvent in ParsedEvents)
            {
                Console.Out.WriteLine(parsedEvent.ToString());
            }
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
