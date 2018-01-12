using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class EDADataset
    {

        public List<EDADatasetEntry> DataEntries;

        public const double SamplingRate = 100; // EDA Sensor samples at a rate of 10 samples per second (or 1 sample every 100 milliseconds)

        public EDADataset()
        {

            DataEntries = new List<EDADatasetEntry>();
        }

        public override string ToString()
        {
            return LabelsToString() + DatasetToString();
        }

        private string LabelsToString()
        {
            string result = "Time";

            result += ";" + "EDA";

            result += "\n";

            return result;
        }

        private string DatasetToString()
        {
            string result = "";

            foreach (EDADatasetEntry entry in DataEntries)
            {
                result += entry.ToString();
                result += "\n";
            }

            return result;
        }

        public Tuple<EDADatasetEntry, EDADatasetEntry> GetPreviousNext(double timestamp)
        {
            EDADatasetEntry previous = null;
            EDADatasetEntry next = null;

            foreach(EDADatasetEntry dataEntry in DataEntries)
            {
                if(dataEntry.Timestamp <= timestamp)
                {
                    previous = dataEntry;
                }
                else if(dataEntry.Timestamp > timestamp)
                {
                    next = dataEntry;
                    break;
                }
            }

            Tuple<EDADatasetEntry, EDADatasetEntry> result = new Tuple<EDADatasetEntry, EDADatasetEntry>(previous, next);

            return result;
        }
    }
}
