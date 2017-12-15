using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBLogParser
{
    class SingleTouchParser
    {
        private List<ADBLogEvent> Events { get; set; }
        public List<Stroke> Strokes { get; set; }

        private Stroke CurrentStroke;
        private Sample CurrentSample;

        public SingleTouchParser(List<ADBLogEvent> EventsToParse)
        {
            Events = EventsToParse;
            Strokes = new List<Stroke>();

            CurrentStroke = null;
            CurrentSample = null;

            Parse();
        }

        private void Parse()
        {
            foreach (ADBLogEvent currentEvent in Events)
            {

                if ((currentEvent.EventType == "BTN_TOUCH") && (currentEvent.EventValue == ADBLogEvent.TOUCH_DOWN) && (CurrentStroke == null))
                {
                    // Start Stroke
                    CurrentStroke = new Stroke();

                } else if ((currentEvent.EventType == "BTN_TOUCH") && (currentEvent.EventValue == ADBLogEvent.TOUCH_UP) && (CurrentStroke != null))
                {
                    // Add Current Stroke to Strokes
                    Strokes.Add(CurrentStroke);
                    
                    // End Stroke
                    CurrentStroke = null;

                } else if((CurrentSample == null) && (currentEvent.OpCode == "EV_ABS"))
                {
                    // Create Sample
                    CurrentSample = new Sample(currentEvent.Timestamp);

                    // Add Feature to Current Sample
                    CurrentSample.AddFeature(currentEvent.EventType, currentEvent.EventValue);

                } else if((CurrentSample != null) && (currentEvent.OpCode == "EV_ABS"))
                {
                    // Add Feature to Current Sample
                    CurrentSample.AddFeature(currentEvent.EventType, currentEvent.EventValue);

                } else if((CurrentSample != null) && (currentEvent.EventType == "SYN_MT_REPORT"))
                {
                    // Add Current Sample to Stroke
                    CurrentStroke.Samples.Add(CurrentSample);

                    // Set Current Sample to null
                    CurrentSample = null;
                }
            }
        }
    }
}
