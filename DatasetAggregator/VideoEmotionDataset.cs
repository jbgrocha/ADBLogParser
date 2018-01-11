using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class VideoEmotionDataset
    {
        public List<VideoEmotionDatasetEntry> DataEntries;

        public const double SamplingRate = 66.0;  // Video Sensor samples at a rate of 15.15152 samples per second (or 1 sample every 66 milliseconds)

        public VideoEmotionDataset()
        {

            DataEntries = new List<VideoEmotionDatasetEntry>();
        }

        public override string ToString()
        {
            return LabelsToString() + DatasetToString();
        }

        private string LabelsToString()
        {
            string result = "";

            result += "Timestamp";

            result += ";" + "Neutral";
            result += ";" + "Happy";
            result += ";" + "Sad";
            result += ";" + "Angry";
            result += ";" + "Surprised";
            result += ";" + "Scared";
            result += ";" + "Disgusted";
            result += ";" + "Contempt";
            result += ";" + "Valence";
            result += ";" + "Arousal";

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
