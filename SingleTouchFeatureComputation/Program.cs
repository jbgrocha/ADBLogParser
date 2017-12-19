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
            Average_X("..\\..\\Resources\\Session-01.json");
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

        private static void Average_X(string filePath)
        {
            Session session = ReadSession(filePath);

            List<double> averages = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                double avg_x = stroke.GetFeatureValuesFromSamples("ABS_MT_POSITION_X").Average();
                averages.Add(avg_x);
            }

            Console.WriteLine("Number of Strokes: " + session.Strokes.Count);

            foreach(double average in averages) {
                Console.WriteLine(average);
            }
        }
    }
}
