using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADBParser;

namespace RawDatasetAggregator
{
    public class SampleParser
    {
        public ADBTouchEventsDataset TouchEvents { get; set; }
        public SampleDataset Dataset { get; set; }

        public SampleParser(ADBTouchEventsDataset touchEvents)
        {
            TouchEvents = touchEvents;
            Dataset = new SampleDataset();
            Parse();
        }

        public void Parse()
        {
            Sample currentSample = new Sample();

            foreach(ADBLogEvent currentEvent in TouchEvents.DataEntries)
            {
                if (currentEvent.EventType == "SYN_REPORT")
                {
                    Dataset.DataEntries.Add(currentSample);
                    currentSample = new Sample();
                }
                else
                {
                    ParseFeature(currentEvent, currentSample);
                }
            }
        }

        public void ParseFeature(ADBLogEvent logEvent, Sample sample)
        {
            if (logEvent.OpCode == "EV_KEY")
            {
                sample.ButtonTouch = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_POSITION_X")
            {
                sample.X = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_POSITION_Y")
            {
                sample.Y = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_TOUCH_MAJOR")
            {
                sample.TouchMajor = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_WIDTH_MAJOR")
            {
                sample.WidthMajor = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_TRACKING_ID")
            {
                sample.MultitouchID = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_TOUCH_MINOR")
            {
                sample.TouchMinor = logEvent.EventValue;
            }
            else if (logEvent.EventType == "ABS_MT_PRESSURE")
            {
                sample.Pressure = logEvent.EventValue;
            }

            sample.Timestamp = logEvent.Timestamp;
        }
    }
}
