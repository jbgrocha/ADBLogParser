using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBLogParser
{
    class Sample
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
    }
}
