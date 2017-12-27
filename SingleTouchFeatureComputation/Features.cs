using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics;
using Sessions;

namespace SingleTouchFeatureComputation
{
    class Features
    {
        private static string X = "ABS_MT_POSITION_X";
        private static string Y = "ABS_MT_POSITION_Y";

        // X features
        public static List<double> Avg_X(Session session)
        {
            return Avg(session, X);
        }

        public static List<double> Min_X(Session session)
        {
            return Min(session, X);
        }

        public static List<double> Max_X(Session session)
        {
            return Max(session, X);
        }

        public static List<double> StandardDeviation_X(Session session)
        {
            return StandardDeviation(session, X);
        }

        //Y features
        public static List<double> Avg_Y(Session session)
        {
            return Avg(session, Y);
        }

        public static List<double> Min_Y(Session session)
        {
            return Min(session, Y);
        }

        public static List<double> Max_Y(Session session)
        {
            return Max(session, Y);
        }

        public static List<double> StandardDeviation_Y(Session session)
        {
            return StandardDeviation(session, Y);
        }

        //  ABS_MT_TOUCH_MAJOR / ABS_MT_WIDTH_MAJOR -> Touch and width relationship -> mojgan called this pressure

        // lengthT -> sum of pitagorean propositions -> layman's version -> sum of the distance between the samples

        // spanX and span Y -> total span in direction of x (probably max - min)

        // distance X and distance Y  -> distance from start to end (probably start.x - end.x)

        // displacement

        //Aux Stat
        public static List<double> Avg(Session session, string feature)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double avg = currentFeature.Average();
                result.Add(avg);
            }

            return result;
        }

        public static List<double> Min(Session session, string feature)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double min = currentFeature.Min();
                result.Add(min);
            }

            return result;
        }

        public static List<double> Max(Session session, string feature)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double max = currentFeature.Max();
                result.Add(max);
            }

            return result;
        }

        public static List<double> StandardDeviation(Session session, string feature)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double std = Measures.StandardDeviation(currentFeature, currentFeature.Mean());
                result.Add(std);
            }

            return result;
        }

        public static List<double> Median(Session session, string feature)
        {

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
        public static List<double> Duration(Session session)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                result.Add(Duration(stroke));
            }

            return result;
        }

        public static double Duration(Stroke stroke)
        {
            double result = stroke.EndTime - stroke.StartTime;
            return result;
        }

        public static List<double> Dist2Prev(Session session)
        {

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

        public static List<double> TimeElapsed(Session session)
        {

            List<double> result = new List<double>();

            foreach (Stroke stroke in session.Strokes)
            {
                double currentStart = stroke.StartTime;

                double sessionStartTime = session.Strokes.First<Stroke>().StartTime;

                result.Add(currentStart - sessionStartTime);
            }

            return result;
        }
    }
}
