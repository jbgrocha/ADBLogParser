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
        public static void Mean_X(Session session)
        {
             Mean(session, X);
        }

        public static void Min_X(Session session)
        {
             Min(session, X);
        }

        public static void Max_X(Session session)
        {
             Max(session, X);
        }

        public static void StandardDeviation_X(Session session)
        {
             StandardDeviation(session, X);
        }

        //Y features
        public static void Mean_Y(Session session)
        {
             Mean(session, Y);
        }

        public static void Min_Y(Session session)
        {
             Min(session, Y);
        }

        public static void Max_Y(Session session)
        {
             Max(session, Y);
        }

        public static void StandardDeviation_Y(Session session)
        {
             StandardDeviation(session, Y);
        }

        //  ABS_MT_TOUCH_MAJOR / ABS_MT_WIDTH_MAJOR -> Touch and width relationship -> mojgan called this pressure

        // lengthT -> sum of pitagorean propositions -> layman's version -> sum of the distance between the samples

        // spanX and span Y -> total span in direction of x (probably max - min)

        // distance X and distance Y  -> distance from start to end (probably start.x - end.x)

        // displacement

        //Aux Stat
        private static void Mean(Session session, string feature)
        {


            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double avg = currentFeature.Average();
                stroke.Features.Add("Mean_" + feature, avg);
            }
        }

        private static void Min(Session session, string feature)
        {
            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double min = currentFeature.Min();
                stroke.Features.Add("Min_" + feature, min);
            }
        }

        private static void Max(Session session, string feature)
        {
            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double max = currentFeature.Max();
                stroke.Features.Add("Max_" + feature, max);
            }
        }

        private static void StandardDeviation(Session session, string feature)
        {
            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double std = Measures.StandardDeviation(currentFeature, currentFeature.Mean());
                stroke.Features.Add("Std_" + feature,std);
            }
        }

        private static void Median(Session session, string feature)
        {
            foreach (Stroke stroke in session.Strokes)
            {
                int[] currentFeature = stroke.GetFeatureValuesFromSamples(feature).ToArray();
                double median = Measures.Median(currentFeature);
                stroke.Features.Add("Median_" + feature, median);
            }
        }

        //Other Features
        public static void Duration(Session session)
        {
            foreach (Stroke stroke in session.Strokes)
            {
                stroke.Features.Add("Duration", Duration(stroke));
            }
        }

        public static double Duration(Stroke stroke)
        {
            double result = stroke.EndTime - stroke.StartTime;
            return result;
        }

        public static void Dist2Prev(Session session)
        {
            double previousEnd = 0.0;
            double currentStart = 0.0;

            foreach (Stroke stroke in session.Strokes)
            {
                currentStart = stroke.StartTime;

                stroke.Features.Add("Dist2Prev", currentStart - previousEnd);

                previousEnd = stroke.EndTime;
            }
        }

        public static void TimeElapsed(Session session)
        {

            foreach (Stroke stroke in session.Strokes)
            {
                double currentStart = stroke.StartTime;

                double sessionStartTime = session.Strokes.First<Stroke>().StartTime;

                stroke.Features.Add("Time_elapsed", currentStart - sessionStartTime);
            }
        }
    }
}
