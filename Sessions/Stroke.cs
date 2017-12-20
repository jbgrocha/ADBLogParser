using System.Collections.Generic;


namespace Sessions
{
    public class Stroke
    {
        public List<Sample> Samples { get; set; }
        public Dictionary<string, double> Features { get; set; }
        public Dictionary<string, int> FeatureSummary { get; set; }


        public Stroke()
        {
            Samples = new List<Sample>();
            Features = new Dictionary<string, double>();
            FeatureSummary = new Dictionary<string, int>();
        }

        public Stroke(List<Sample> StrokeSamples, Dictionary<string, double> features, Dictionary<string,int> sampleFeatureSummary)
        {
            Samples = StrokeSamples;
            Features = features;
            FeatureSummary = sampleFeatureSummary;
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

        public List<int> GetFeatureValuesFromSamples(string key)
        {
            List<int> result = new List<int>();

            foreach (Sample sample in Samples)
            {
                int feature = sample.Features[key];
                result.Add(feature);
            }

            return result;
        }
    }
}
