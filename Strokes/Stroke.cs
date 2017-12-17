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

        public Stroke()
        {
            Samples = new List<Sample>();
            Features = new Dictionary<string, int>();
        }

        public Stroke(List<Sample> StrokeSamples, Dictionary<string, int> features)
        {
            Samples = StrokeSamples;
            Features = features;
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
