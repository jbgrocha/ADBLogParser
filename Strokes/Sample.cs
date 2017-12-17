using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strokes
{
    public class Sample
    {
        public double TimeStamp { get; set; }
        public Dictionary<string, int> Features { get; set; }

        public Sample() {
            TimeStamp = 0.0;
            Features = new Dictionary<string, int>();
        }

        public Sample(double SampleTimeStamp)
        {
            TimeStamp = SampleTimeStamp;
            Features = new Dictionary<string, int>();
        }

        public Sample(double SampleTimeStamp, Dictionary<string, int> SampleFeatures)
        {
            TimeStamp = SampleTimeStamp;
            Features = SampleFeatures;
        }

        public void AddFeature(string key, int value)
        {
            if(!Features.ContainsKey(key))
            {
                Features.Add(key, value);
            }
        }

        public override string ToString()
        {
            string result = "Sample - " + TimeStamp + "\n";

            foreach(KeyValuePair<string, int> feature in Features)
            {
                result += feature.Key + " - " + feature.Value + "\n";
            }

            return result;
        }
    }
}
