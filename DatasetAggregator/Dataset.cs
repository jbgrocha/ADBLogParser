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
    }
}
