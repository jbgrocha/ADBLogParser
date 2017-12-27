using Accord.Statistics;
using Sessions;
using SingleTouchSessionParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleTouchFeatureComputation
{
    class Program
    {
        private static string X = "ABS_MT_POSITION_X";
        private static string Y = "ABS_MT_POSITION_Y";


        public static void Main(string[] args)
        {
            //PrintSessionToJSON("..\\..\\Resources\\Session-01.json");
            //List<double> test = Median("..\\..\\Resources\\Session-01.json", "ABS_MT_POSITION_X");
            List<double> test = TimeElapsed("..\\..\\Resources\\Session-01.json");

            foreach (double value in test)
            {
                Console.WriteLine(value);
            }

        }

        // X features
        private static List<double> Avg_X(string filePath)
        {
            return Avg(filePath, X);
        }

        private static List<double> Min_X(string filePath)
        {
            return Min(filePath, X);
        }

        private static List<double> Max_X(string filePath)
        {
            return Max(filePath, X);
        }

        private static List<double> StandardDeviation_X(string filePath)
        {
            return StandardDeviation(filePath, X);
        }

        //Y features
        private static List<double> Avg_Y(string filePath)
        {
            return Avg(filePath, Y);
        }

        private static List<double> Min_Y(string filePath)
        {
            return Min(filePath, Y);
        }

        private static List<double> Max_Y(string filePath)
        {
            return Max(filePath, Y);
        }

        private static List<double> StandardDeviation_Y(string filePath)
        {
            return StandardDeviation(filePath, Y);
        }

        //Aux Stat
        private static List<double> Avg(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double avg = currentFeature.Average();
                result.Add(avg);
            }

            return result;
        }

        private static List<double> Min(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double min = currentFeature.Min();
                result.Add(min);
            }

            return result;
        }

        private static List<double> Max(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double max = currentFeature.Max();
                result.Add(max);
            }

            return result;
        }

        private static List<double> StandardDeviation(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double std = Measures.StandardDeviation(currentFeature, currentFeature.Mean());
                result.Add(std);
            }

            return result;
        }

        private static List<double> Median(string filePath, string feature)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double median = Measures.Median(currentFeature);
                result.Add(median);
            }

            return result;
        }

        //Other Features
        private static List<double> Duration(string filePath)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                result.Add(Duration(stroke));
            }

            return result;
        }

        private static double Duration(Stroke stroke)
        {
            double result = stroke.EndTime - stroke.StartTime;
            return result;
        }

        private static List<double> Dist2Prev(string filePath)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            double previousEnd = 0.0;
            double currentStart = 0.0;

            foreach (Stroke stroke in session.Strokes)
            {
                currentStart = stroke.StartTime;

                result.Add(currentStart - previousEnd);

                previousEnd = stroke.EndTime;
            }

            return result;
        }

        private static List<double> TimeElapsed(string filePath)
        {
            Session session = ReadSession(filePath);

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                double currentStart = stroke.StartTime;

                double sessionStartTime = session.Strokes.First<Stroke>().StartTime;

                result.Add(currentStart - sessionStartTime);
            }

            return result;
        }

        //Generic operations
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

    }
}
