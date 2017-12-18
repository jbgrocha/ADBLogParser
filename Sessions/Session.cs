using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Strokes
{
    public class Session
    {
        public string FilePath { get; set; }
        public List<Stroke> Strokes { get; set; }

        public Session(string filepath)
        {
            FilePath = filepath;
        }

        public Session(string filepath, List<Stroke> strokes)
        {
            FilePath = filepath;
            Strokes = strokes;
        }

        public string FeatureSummaryToJSON()
        {
            return JsonConvert.SerializeObject(FeatureSummary(), Formatting.Indented);
        }

        public Dictionary<string, int> FeatureSummary()
        {
            Dictionary<string, int> featureSummary = new Dictionary<string, int>();

            foreach (Stroke stroke in Strokes)
            {
                foreach (KeyValuePair<string, int> feature in stroke.SampleFeatureSummary.ToList())
                {
                    if (!featureSummary.ContainsKey(feature.Key))
                    {
                        featureSummary.Add(feature.Key, feature.Value);

                    }
                    else
                    {

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
    }
}
