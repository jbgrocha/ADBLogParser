using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleTouchSessionParser;
using Sessions;
using Accord.Statistics;

namespace SingleTouchFeatureComputation
{
    class Program
    {
        public static void Main(string[] args)
        {
            //PrintSessionToJSON("..\\..\\Resources\\Session-01.json");
            List<double> test = Max("..\\..\\Resources\\Session-01.json", "ABS_MT_POSITION_X");

            foreach(double value in test)
            {
                Console.WriteLine(value);
            }

        }

        private static void PrintSessionToJSON(string filePath)
        {
            Session session = ReadSession(filePath);
            Console.Write(session.ToJSON());
        }

        private static Session ReadSession(string filePath)
        {
            SessionParser sessionParser = new SessionParser(filePath);
            return sessionParser.Session;
        }

        private static List<double> Avg(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> avgs = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double avg = currentFeature.Average();
                avgs.Add(avg);
            }

            return avgs;
        }

        private static List<double> Min(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> mins = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double min = currentFeature.Min();
                mins.Add(min);
            }

            return mins;
        }

        private static List<double> Max(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> maxs = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double max = currentFeature.Max();
                maxs.Add(max);
            }

            return maxs;
        }

        private static List<double> StandardDeviation(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> stds = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double std = Measures.StandardDeviation(currentFeature, currentFeature.Mean());
                stds.Add(std);
            }

            return stds;
        }

        private static List<double> Median(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> medians = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double median = Measures.Median(currentFeature);
                medians.Add(median);
            }

            return medians;
        }
    }
}
