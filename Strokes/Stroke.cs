using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strokes
{
    public class Stroke
    {
        public List<Sample> Samples { get; set; }
        public Dictionary<string, int> Features { get; set; }
        public Dictionary<string, int> SampleFeatureSummary { get; set; }


        public Stroke()
        {
            Samples = new List<Sample>();
            Features = new Dictionary<string, int>();
            SampleFeatureSummary = new Dictionary<string, int>();
        }

        public Stroke(List<Sample> StrokeSamples, Dictionary<string, int> features, Dictionary<string,int> sampleFeatureSummary)
        {
            Samples = StrokeSamples;
            Features = features;
            SampleFeatureSummary = sampleFeatureSummary;
        }

        public override string ToString()
        {
            string result = "Stroke\n";

            foreach (Sample sample in Samples)
            {
                result += sample;
            }

            return result;
        }
    }
}
