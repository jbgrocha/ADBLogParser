using RawDatasetGenerator;
using StrokeDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetParser
{
    public static class StrokeFeatureAdder
    {
        public static void AddFeatures(List<Stroke> strokes)
        {
            foreach (Stroke stroke in strokes)
            {
                // get base feature arrays for the stroke
                BaseFeatures baseFeature = ComputeBaseFeatures(stroke);

                // compute auxiliary arrays?

                // compute features
            }
        }

        public static BaseFeatures ComputeBaseFeatures(Stroke stroke)
        {
            List<double> timestamp = new List<double>();

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            List<double> touchMajor = new List<double>();
            List<double> widthMajor = new List<double>();

            // currently assuming that TOUCH_UP is not inserted into RawDatasetEntries
            foreach (RawDatasetEntry entry in stroke.RawDatasetEntries)
            {
                timestamp.Add(entry.TouchSample.Timestamp);

                x.Add((double) entry.TouchSample.X);
                y.Add((double) entry.TouchSample.Y);

                touchMajor.Add((double) entry.TouchSample.TouchMajor);
                widthMajor.Add((double) entry.TouchSample.WidthMajor);
            }

            BaseFeatures baseFeatures = new BaseFeatures(timestamp, x, y, touchMajor, widthMajor);

            return baseFeatures;
        }

        public static void ComputeFeatures(BaseFeatures baseFeatures)
        {
            // Article base features
            // Stroke length
            // sum of sqrt(diff_x^2 + diff_y^2)

            // Stroke speed -> Avg
            // length / (last touch features time - first touch features time)

            // Directness index (distance between start and end -> ignores path) -> Avg
            double directness = Distance(baseFeatures.X.First(), baseFeatures.X.Last(), baseFeatures.Y.First(), baseFeatures.Y.Last());

            // Contact Area (Touch Major) -> Avg
            double contactArea = baseFeatures.TouchMajor.Average();
        }

        // aux distance between 2 points
        public static double Distance(double x_start, double x_end, double y_start, double y_end)
        {
            double result = 0.0;

            double x = x_end - x_start;
            double y = y_end - y_start;

            result = Pythagorean(x, y);

            return result;
        }

        public static double Pythagorean(double x, double y)
        {
            double result = 0.0;

            result = Math.Sqrt(x * x + y * y);

            return result;
        }
    }    
}
