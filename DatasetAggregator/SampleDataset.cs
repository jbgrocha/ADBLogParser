using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawDatasetAggregator
{
    public class SampleDataset
    {
        public List<Sample> DataEntries;

        public SampleDataset()
        {
            DataEntries = new List<Sample>();
        }

        public override string ToString()
        {
            string result = "";

            foreach(Sample entry in DataEntries)
            {
                result += entry.ToString() + "\n";
            }

            return result;
        }
    }
}
