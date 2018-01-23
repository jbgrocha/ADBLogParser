using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetParser
{
    public class Stroke
    {
        public List<RawDatasetEntry> RawDatasetEntries { get; set; }
        public Dictionary<string, double> Features { get; set; }
        public Dictionary<string, double> Labels { get; set; }
        public Dictionary<string, string> TextLabels { get; set; }

        public Stroke()
        {
            RawDatasetEntries = new List<RawDatasetEntry>();
            Features = new Dictionary<string, double>();
            Labels = new Dictionary<string, double>();
            TextLabels = new Dictionary<string, string>();
        }

        public Stroke(List<RawDatasetEntry> rawDatasetEntries)
        {
            RawDatasetEntries = rawDatasetEntries;
            Features = new Dictionary<string, double>();
            Labels = new Dictionary<string, double>();
            TextLabels = new Dictionary<string, string>();
        }
    }
}
