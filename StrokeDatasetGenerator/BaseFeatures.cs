using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetParser
{
    public class BaseFeatures
    {
        public List<double> Timestamp { get; set; }

        public List<double> X { get; set; }
        public List<double> Y { get; set; }

        public List<double> TouchMajor { get; set; }
        public List<double> WidthMajor { get; set; }

        public BaseFeatures()
        {
            Timestamp = new List<double>();

            X = new List<double>();
            Y = new List<double>();

            TouchMajor = new List<double>();
            WidthMajor = new List<double>();
        }

        public BaseFeatures(List<double> timestamp, List<double> x, List<double> y, List<double> touchMajor, List<double> widthMajor)
        {
            Timestamp = timestamp;

            X = x;
            Y = y;

            TouchMajor = touchMajor;
            WidthMajor = widthMajor;
        }
    }
}
