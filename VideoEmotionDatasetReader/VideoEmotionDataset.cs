using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEmotionDatasetParser
{
    public class VideoEmotionDataset
    {
        public List<string> Labels { get; set; }
        public List<List<double>> Values { get; set; }

        public VideoEmotionDataset()
        {
            Labels = new List<string>();
            Values = new List<List<double>>();
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

            foreach (List<double> datasetEntry in Values)
            {
                result += DataEntryToString(datasetEntry);
                result += "\n";
            }

            return result;
        }

        private string DataEntryToString(List<double> datasetEntry)
        {
            string result = "";

            for (int i = 0; i < datasetEntry.Count; i++)
            {
                result += datasetEntry.ElementAt(i);

                if (i != datasetEntry.Count - 1)
                {
                    result += ";";
                }
            }

            return result;
        }
    }
}
