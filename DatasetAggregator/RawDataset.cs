using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EDAParser;
using VideoParser;
using SampleParser;

namespace RawDatasetGenerator
{
    public class RawDataset
    {
        public List<RawDatasetEntry> Entries { get; set; }
        public string SessionId { get; set; }

        public const string CSVHeaders = "Session"+ ";" + Sample.Headers + ";" + VideoEmotionDatasetEntry.PreviousHeaders + ";" + 
            VideoEmotionDatasetEntry.NextHeaders + ";" + EDADatasetEntry.PreviousHeaders + ";" + EDADatasetEntry.NextHeaders;

        public RawDataset(string id)
        {
            SessionId = id;
            Entries = new List<RawDatasetEntry>();
        }

        public override string ToString()
        {
            string result = "";

            foreach(RawDatasetEntry entry in Entries)
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
