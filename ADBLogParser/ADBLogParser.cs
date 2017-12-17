using ADBLogParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Strokes;

namespace LogParser
{
    class ADBLogParser
    {
        private string FilePath { get; set; }
        private List<string> FileLines { get; set; }
        private List<string[]> UnparsedEvents { get; set; }
        private List<ADBLogEvent> ParsedEvents { get; set; }

        private List<Stroke> Strokes { get; set; }

        public ADBLogParser(string filePath)
        {
            FilePath = filePath;
            ReadFile();
            DiscardLines();
            CleanUpLines();
            ReadUnparsedEvents();
            ParseLogEvents();

            ParseStrokes();

        }

        public string StrokesToJSON()
        {
            return JsonConvert.SerializeObject(Strokes, Formatting.Indented);
        }

        public void PrintStrokes()
        {
            foreach (Stroke stroke in Strokes)
            {
                Console.Out.WriteLine(stroke);
            }
        }

        private void ParseStrokes()
        {
            SingleTouchParser TouchParser = new SingleTouchParser(ParsedEvents);
            Strokes = TouchParser.Strokes;
        }

        private void CalculateTouchSummary(Dictionary<string, int> TouchSummary)
        {
            foreach (ADBLogEvent logEvent in ParsedEvents)
            {
                AddTouchToSummary(logEvent, TouchSummary);
            }
        }

        private void AddTouchToSummary(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            if ((logEvent.OpCode == "EV_SYN") || (logEvent.EventType == "ABS_MT_TRACKING_ID"))
            {
                AddTouchEvent(logEvent, TouchSummary);
            }
        }

        private void AddTouchEvent(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            string key = logEvent.EventType + " " + logEvent.EventValue;

            if(!TouchSummary.ContainsKey(key))
            {
                TouchSummary.Add(key, 1);
            }
            else
            {
                TouchSummary[key] += 1;
            }
        }

        public void PrintTouchSummary()
        {
            Dictionary<string, int> TouchSummary = new Dictionary<string, int>();
            
            CalculateTouchSummary(TouchSummary);

            foreach (KeyValuePair<string, int> existingMultiTouch in TouchSummary)
            {
                Console.Out.WriteLine(existingMultiTouch.Key + ": " + existingMultiTouch.Value);
            }
        }

        private void CalculateFeatureSummary(Dictionary<string, int> FeatureSummary)
        {
            foreach (ADBLogEvent logEvent in ParsedEvents)
            {
                AddFeatureToSummary(logEvent, FeatureSummary);
            }
        }

        private void AddFeatureToSummary(ADBLogEvent logEvent, Dictionary<string, int> FeatureSummary)
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

        public void PrintFeatureSummary()
        {
            Dictionary<string, int> FeatureSummary = new Dictionary<string, int>();

            CalculateFeatureSummary(FeatureSummary);

            foreach (KeyValuePair<string, int> existingFeature in FeatureSummary)
            {
                Console.Out.WriteLine(existingFeature.Key + " " + existingFeature.Value);
            }
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
            foreach(string[] unparsedEvent in UnparsedEvents)
            {
                PrintUnparsedEvent(unparsedEvent);
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

            for (int i = 0; i < FileLines.Count; i++ )
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
            foreach(string line in FileLines)
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
