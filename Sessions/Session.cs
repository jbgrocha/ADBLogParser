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
        public Dictionary<string, int> Summary { get; set; }

        public Session(string filepath)
        {
            FilePath = filepath;
            Strokes = new List<Stroke>();
            Summary = new Dictionary<string, int>();
        }

        public Session(string filepath, List<Stroke> strokes, Dictionary<string, int> summary)
        {
            FilePath = filepath;
            Strokes = strokes;
            Summary = summary;
        }

        public string FeatureSummaryToJSON()
        {
            return JsonConvert.SerializeObject(Summary, Formatting.Indented);
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
