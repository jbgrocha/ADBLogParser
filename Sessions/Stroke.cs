using System.Collections.Generic;


namespace Sessions
{
    public class Stroke
    {
        public List<Sample> Samples { get; set; }
        public Dictionary<string, int> Features { get; set; }
        public Dictionary<string, int> Summary { get; set; }


        public Stroke()
        {
            Samples = new List<Sample>();
            Features = new Dictionary<string, int>();
            Summary = new Dictionary<string, int>();
        }

        public Stroke(List<Sample> StrokeSamples, Dictionary<string, int> features, Dictionary<string,int> sampleFeatureSummary)
        {
            Samples = StrokeSamples;
            Features = features;
            Summary = sampleFeatureSummary;
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
