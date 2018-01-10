using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class VideoEmotionDataset
    {
        public List<string> Labels { get; set; }

        public List<VideoEmotionDatasetEntry> DataEntries;

        public const double SamplingRate = 66.0;  // Video Sensor samples at a rate of 15.15152 samples per second (or 1 sample every 66 milliseconds)

        public VideoEmotionDataset()
        {
            Labels = new List<string>();

            DataEntries = new List<VideoEmotionDatasetEntry>();
        }

        public override string ToString()
        {
            return LabelsToString() + DatasetToString();
        }

        private string LabelsToString()
        {
            string result = "";

            for (int i = 0; i < Labels.Count; i++)
            {
                result += Labels.ElementAt(i);

                if (i != Labels.Count - 1)
                {
                    result += ";";
                }
            }

            result += "\n";

            return result;
        }

        private string DatasetToString()
        {
            string result = "";

            foreach (VideoEmotionDatasetEntry entry in DataEntries)
            {
                result += entry.ToString();
                result += "\n";
            }

            return result;
        }
    }
}
