using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    public class FeatureComputation
    {
        public List<double> Timestamp { get; set; }

        public List<double> X { get; set; }
        public List<double> Y { get; set; }

        public List<double> TouchMajor { get; set; }
        public List<double> WidthMajor { get; set; }

        public Dictionary<string, double?> Features { get; set; }

        public FeatureComputation(Stroke stroke)
        {
            Timestamp = new List<double>();

            X = new List<double>();
            Y = new List<double>();

            TouchMajor = new List<double>();
            WidthMajor = new List<double>();

            Features = new Dictionary<string, double?>();

            // currently assuming that TOUCH_UP is not inserted into RawDatasetEntries
            foreach (RawDatasetEntry entry in stroke.RawDatasetEntries)
            {
                Timestamp.Add(entry.TouchSample.Timestamp);

                X.Add((double)entry.TouchSample.X);
                Y.Add((double)entry.TouchSample.Y);

                TouchMajor.Add((double)entry.TouchSample.TouchMajor);
                WidthMajor.Add((double)entry.TouchSample.WidthMajor);
            }
        }

        public void ComputeFeatures()
        {
            // Article base features
            // Stroke length
            // sum of sqrt(diff_x^2 + diff_y^2)

            // Stroke speed -> Avg
            // length / (last touch features time - first touch features time)

            // Directness index (distance between start and end -> ignores path) -> Avg
            double directness = Distance(X.First(), X.Last(), Y.First(), Y.Last());
            Features.Add("directness", directness);

            // Contact Area (Touch Major) -> Avg
            double contactArea = TouchMajor.Average();
            Features.Add("contactArea", contactArea);
        }

        // aux distance between 2 points
        private double Distance(double x_start, double x_end, double y_start, double y_end)
        {
            double result = 0.0;

            double x = x_end - x_start;
            double y = y_end - y_start;

            result = Pythagorean(x, y);

            return result;
        }

        // aux pythagorena theorem
        private double Pythagorean(double x, double y)
        {
            double result = 0.0;

            result = Math.Sqrt(x * x + y * y);

            return result;
        }
    }
}
