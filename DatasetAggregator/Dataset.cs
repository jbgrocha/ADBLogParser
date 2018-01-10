using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class Dataset
    {
        public List<DatasetEntry> Entries { get; set; }

        public Dataset()
        {
            Entries = new List<DatasetEntry>();
        }

        public override string ToString()
        {
            string result = "";

            foreach(DatasetEntry entry in Entries)
            {
                result += entry.ToString() + "\n";
            }

            return result;
        }
    }
}
