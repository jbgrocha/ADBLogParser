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
        public string SessionId { get; set; }

        public const string CSVHeaders = "Session"+ ";" + Sample.Headers + ";" + VideoEmotionDatasetEntry.PreviousHeaders + ";" + 
            VideoEmotionDatasetEntry.NextHeaders + ";" + EDADatasetEntry.PreviousHeaders + ";" + EDADatasetEntry.NextHeaders;

        public Dataset(string id)
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
