using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleTouchSessionParser;
using Sessions;

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
                double avg_x = stroke.GetFeatureValuesFromSamples(feature).Average();
                avgs.Add(avg_x);
            }

            return avgs;
        }

        private static List<double> Min(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> mins = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                double min_x = stroke.GetFeatureValuesFromSamples(feature).Min();
                mins.Add(min_x);
            }

            return mins;
        }

        private static List<double> Max(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> maxs = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                double max_x = stroke.GetFeatureValuesFromSamples(feature).Max();
                maxs.Add(max_x);
            }

            return maxs;
        }
    }
}
