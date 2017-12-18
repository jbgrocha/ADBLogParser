using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Sessions
{
    public class Session
    {
        public string FilePath { get; set; }
        public List<Stroke> Strokes { get; set; }

        // FeatureSummary should be an indicator of the data in the session, and not something that can be serialized and deserialized
        // meaning, it should be dynamically calculated from the data present in the session
        public Dictionary<string, int> FeatureSummary { get; set; }

        public int NumberOfSamples;

        public Session(string filepath)
        {
            FilePath = filepath;
            Strokes = new List<Stroke>();
            FeatureSummary = new Dictionary<string, int>();
            NumberOfSamples = 0;
        }

        [JsonConstructor]
        public Session(string filepath, List<Stroke> strokes, Dictionary<string, int> summary, int numberOfSamples)
        {
            FilePath = filepath;
            Strokes = strokes;
            FeatureSummary = summary;
            NumberOfSamples = numberOfSamples;
        }

        // ValidateSession should be calculated dynamically and not be calculated during the parsing
        // The purpose is to validate the session data, if everything is already pre calculated, 
        // the session's integrity cannot be accurately validated
        public bool ValidateSession()
        {
            bool result = true;

            foreach(KeyValuePair<string, int> feature in FeatureSummary)
            {
                if(feature.Value != NumberOfSamples)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public string FeatureSummaryToJSON()
        {
            return JsonConvert.SerializeObject(FeatureSummary, Formatting.Indented);
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
    }
}
