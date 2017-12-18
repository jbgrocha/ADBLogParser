using System.Collections.Generic;
using Sessions;

namespace ADBLogParser
{
    class SingleTouchParser
    {
        private List<ADBLogEvent> Events { get; set; }
        public Session Session { get; set; }

        private Stroke CurrentStroke;
        private Sample CurrentSample;

        public SingleTouchParser(List<ADBLogEvent> EventsToParse, Session session)
        {
            Events = EventsToParse;
            Session = session;

            CurrentStroke = null;
            CurrentSample = null;

            Parse();
        }

        private void AddFeatureToStrokeSummary(string key, int value)
        {
            if (!CurrentStroke.FeatureSummary.ContainsKey(key))
            {
                CurrentStroke.FeatureSummary.Add(key, 1);
            }
            else
            {
                CurrentStroke.FeatureSummary[key] += 1;
            }
        }

        private void AddFeatureToSessionSummary(string key, int value)
        {
            if (!Session.FeatureSummary.ContainsKey(key))
            {
                Session.FeatureSummary.Add(key, 1);
            }
            else
            {
                Session.FeatureSummary[key] += 1;
            }
        }

        private void AddFeatureToSample(string key, int value)
        {
            // Add Feature to Current Sample
            CurrentSample.AddFeature(key, value);

            // Add or Increment Feature in Stroke Feature Summary
            AddFeatureToStrokeSummary(key, value);

            // Add or Increment Feature in Session Feature Summary
            AddFeatureToSessionSummary(key, value);
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
                    // Add Current Stroke to Session.Strokes
                    Session.Strokes.Add(CurrentStroke);
                    
                    // End Stroke
                    CurrentStroke = null;

                } else if((CurrentSample == null) && (currentEvent.OpCode == "EV_ABS"))
                {
                    // Create Sample
                    CurrentSample = new Sample(currentEvent.Timestamp);

                    // Add Feature to Current Sample
                    AddFeatureToSample(currentEvent.EventType, currentEvent.EventValue);

                } else if((CurrentSample != null) && (currentEvent.OpCode == "EV_ABS"))
                {
                    // Add Feature to Current Sample
                    AddFeatureToSample(currentEvent.EventType, currentEvent.EventValue);

                }
                else if((CurrentSample != null) && (currentEvent.EventType == "SYN_MT_REPORT"))
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
