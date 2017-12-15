using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBLogParser
{
    class Stroke
    {
        public List<Sample> Samples { get; set; }

        public Stroke()
        {
            Samples = new List<Sample>();
        }

        public Stroke(List<Sample> StrokeSamples)
        {
            Samples = StrokeSamples;
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
