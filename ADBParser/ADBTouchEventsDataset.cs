﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBParser
{
    public class ADBTouchEventsDataset
    {
        public List<ADBLogEvent> DataEntries;

        public ADBTouchEventsDataset()
        {
            DataEntries = new List<ADBLogEvent>();
        }

        private void CalculateTouchEventsSummary(Dictionary<string, int> TouchSummary)
        {
            foreach (ADBLogEvent logEvent in DataEntries)
            {
                AddTouchEventToSummary(logEvent, TouchSummary);
            }
        }

        private void AddTouchEventToSummary(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            if ((logEvent.OpCode == "EV_SYN") || (logEvent.EventType == "ABS_MT_TRACKING_ID"))
            {
                AddTouchEvent(logEvent, TouchSummary);
            }
        }

        private void AddTouchEvent(ADBLogEvent logEvent, Dictionary<string, int> TouchSummary)
        {
            string key = logEvent.EventType + " " + logEvent.EventValue;

            if (!TouchSummary.ContainsKey(key))
            {
                TouchSummary.Add(key, 1);
            }
            else
            {
                TouchSummary[key] += 1;
            }
        }

        public string TouchEventSummary()
        {
            Dictionary<string, int> TouchSummary = new Dictionary<string, int>();

            CalculateTouchEventsSummary(TouchSummary);

            string result = "";

            foreach (KeyValuePair<string, int> existingMultiTouch in TouchSummary)
            {
                result += existingMultiTouch.Key + ": " + existingMultiTouch.Value;
            }

            return result;
        }

        private void CalculateFeatureEventSummary(Dictionary<string, int> FeatureSummary)
        {
            foreach (ADBLogEvent logEvent in DataEntries)
            {
                AddFeatureEventToSummary(logEvent, FeatureSummary);
            }
        }

        private void AddFeatureEventToSummary(ADBLogEvent logEvent, Dictionary<string, int> FeatureSummary)
        {
            if (logEvent.OpCode == "EV_ABS")
            {
                string key = logEvent.EventType;

                if (!FeatureSummary.ContainsKey(key))
                {
                    FeatureSummary.Add(key, 1);
                }
                else
                {
                    FeatureSummary[key] += 1;
                }
            }
        }

        public string FeatureEventSummary()
        {
            Dictionary<string, int> FeatureSummary = new Dictionary<string, int>();

            CalculateFeatureEventSummary(FeatureSummary);

            string result = "";

            foreach (KeyValuePair<string, int> existingFeature in FeatureSummary)
            {
                result += existingFeature.Key + " " + existingFeature.Value;
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            foreach (ADBLogEvent parsedEvent in DataEntries)
            {
                result += parsedEvent.ToString() + "\n";
            }

            return result;
        }

        public string ValidateSync()
        {
            string result = "";

            ADBLogEvent previous = null;

            foreach(ADBLogEvent entry in DataEntries)
            {
                if((previous != null) && (previous.EventType == "SYN_MT_REPORT") && (entry.EventType != "SYN_REPORT"))
                {
                    result += previous.ToString() + " -> " + entry.ToString() + "\n";
                }

                previous = entry;

            }

            return result;
        }
    }
}
