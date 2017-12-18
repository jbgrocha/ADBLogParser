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
