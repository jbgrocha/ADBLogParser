using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADBParser;

namespace SampleParser
{
    public class SampleDatasetParser
    {
        public ADBTouchEventsDataset TouchEvents { get; set; }
        public SampleDataset Dataset { get; set; }

        public SampleDatasetParser(ADBTouchEventsDataset touchEvents)
        {
            TouchEvents = touchEvents;
            Dataset = new SampleDataset();
            Parse();
        }

        // Parse needs fixing, SYN_MT_REPORT vs SYN_REPORT
        // think the only moment SYN_REPORT appears by itself is after a touch up (end of stroke)
        public void Parse()
        {
            Sample currentSample = new Sample();

            ADBLogEvent previousEvent = null;

            foreach(ADBLogEvent currentEvent in TouchEvents.DataEntries)
            {
                // This is the only normal situation where we have a SYN_REPORT without a SYN_MT_REPORT before it
                if ((currentEvent.EventType == "SYN_REPORT") && (previousEvent.EventType != "SYN_MT_REPORT"))
                {
                    Dataset.DataEntries.Add(currentSample);
                    currentSample = new Sample();
                }
                else if(currentEvent.EventType == "SYN_MT_REPORT")
                {
                    Dataset.DataEntries.Add(currentSample);
                    currentSample = new Sample();
                }
                else
                {
                    ParseFeature(currentEvent, currentSample);
                }

                previousEvent = currentEvent;
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
