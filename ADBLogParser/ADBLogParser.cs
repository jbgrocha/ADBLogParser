using ADBLogParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Strokes;
using System.Linq;

namespace LogParser
{
    class ADBLogParser
    {
        private string FilePath { get; set; }
        private List<Stroke> Strokes { get; set; }
        public List<ADBLogEvent> ParsedEvents { get; set; }

        public ADBLogParser(string filePath)
        {
            FilePath = filePath;
            ParseEvents();
            ParseStrokes();

        }

        public string FeatureSummaryToJSON()
        {
            return JsonConvert.SerializeObject(FeatureSummary(), Formatting.Indented);
        }

        public Dictionary<string,int> FeatureSummary()
        {
            Dictionary<string, int> featureSummary = new Dictionary<string, int>();

            foreach(Stroke stroke in Strokes)
            {
                foreach (KeyValuePair<string, int> feature in stroke.SampleFeatureSummary.ToList())
                {
                    if (!featureSummary.ContainsKey(feature.Key))
                    {
                        featureSummary.Add(feature.Key, feature.Value);

                    } else {

                        featureSummary[feature.Key] += feature.Value;
                    }
                }
            }

            return featureSummary;
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

            if (!TouchSummary.ContainsKey(key))
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

        public void PrintParsedEvents()
        {
            foreach (ADBLogEvent parsedEvent in ParsedEvents)
            {
                Console.Out.WriteLine(parsedEvent.ToString());
            }
        }

        private void ParseStrokes()
        {
            SingleTouchParser TouchParser = new SingleTouchParser(ParsedEvents);
            Strokes = TouchParser.Strokes;
        }

        private void ParseEvents()
        {
            EventParser eventParser = new EventParser(FilePath);
            ParsedEvents = eventParser.ParsedEvents;
        }

    }
}
