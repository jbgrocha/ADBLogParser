using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatasetAggregator
{
    public class Dataset
    {
        public List<DatasetEntry> Entries { get; set; }
        public int SessionId { get; set; }

        public Dataset(int id)
        {
            SessionId = id;
            Entries = new List<DatasetEntry>();
        }

        public override string ToString()
        {
            string result = "";

            foreach(DatasetEntry entry in Entries)
            {
                result += SessionId + ";" + entry.ToString() + "\n";
            }

            return result;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
