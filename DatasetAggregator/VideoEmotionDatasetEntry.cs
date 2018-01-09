using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class VideoEmotionDatasetEntry
    {
        public double Timestamp { get; set;}
        public Dictionary<string, double> Labels { get; set; }

        public VideoEmotionDatasetEntry()
        {
            Timestamp = 0.0;
            Labels = new Dictionary<string, double>();
        }

        public VideoEmotionDatasetEntry(double timestamp, Dictionary<string, double> labels)
        {
            Timestamp = timestamp;
            Labels = labels;
        }

        public override string ToString()
        {
            string result = Timestamp.ToString();

            foreach(KeyValuePair<string, double> label in Labels)
            {
                result += ";" + label.Value;
            }

            return result;
        }
    }
}
