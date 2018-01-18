using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawDatasetAggregator
{
    public class Sample
    {
        public double Timestamp { get; set; }

        public int? ButtonTouch { get; set; }

        public int? MultitouchID { get; set; }

        public int? X { get; set; }
        public int? Y { get; set; }

        public int? TouchMajor { get; set; }
        public int? WidthMajor { get; set; }

        public int? TouchMinor { get; set; }
        public int? Pressure {get; set;}

        public const int TOUCH_UP = 0;
        public const int TOUCH_DOWN = 1;

        public const string Headers = "Timestamp;ButtonTouch;MultitouchID;X;Y;TouchMajor;WidthMajor;TouchMinor;Pressure";

        public Sample()
        {
            Timestamp = 0.0;

            ButtonTouch = null;

            MultitouchID = null;

            X = null;
            Y = null;

            TouchMajor = null;
            WidthMajor = null;

            TouchMinor = null;
            Pressure = null;
        }

        public Sample(double timestamp)
        {
            Timestamp = timestamp;

            ButtonTouch = null;

            MultitouchID = null;

            X = null;
            Y = null;

            TouchMajor = null;
            WidthMajor = null;

            TouchMinor = null;
            Pressure = null;
        }
    
        public Sample(double timestamp, int? buttonTouch, int? multitouchID, int? x, int? y, int? touchMajor, int? widthMajor, int? touchMinor, int? pressure)
        {
            Timestamp = timestamp;

            ButtonTouch = buttonTouch;

            MultitouchID = multitouchID;

            X = x;
            Y = y;

            TouchMajor = touchMajor;
            WidthMajor = widthMajor;

            TouchMinor = touchMinor;
            Pressure = pressure;
        }

        public override string ToString()
        {
            string result = "";

            result += Timestamp.ToString();

            result += ";" + ButtonTouch.ToString();

            result += ";" + MultitouchID.ToString();

            result += ";" + X.ToString();
            result += ";" + Y.ToString();

            result += ";" + TouchMajor.ToString();
            result += ";" + WidthMajor.ToString();

            result += ";" + TouchMinor.ToString();
            result += ";" + Pressure.ToString();

            return result;
        }
    }
}
