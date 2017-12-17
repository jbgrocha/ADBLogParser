using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBLogParser
{
    class ADBLogEvent
    {
        /* 
         * @jbgrocha: I'm suspicious of 'timestamp' implementation 
         * I suspect issues may occur when I attempt to synchronize the
         * emotion data or the EDA data with 
         */ 
        public double Timestamp { get; set; }
        public string Device { get; set; }
        public string OpCode { get; set; }
        public string EventType { get; set; }
        public int EventValue { get; set; }

        public const int TOUCH_UP = 0;
        public const int TOUCH_DOWN = 1;

        public ADBLogEvent(string[] eventText)
        {
            SetTimestamp(eventText[0]);

            Device = eventText[1];

            OpCode = eventText[2];

            EventType = eventText[3];
            
            SetEventValue(eventText[4]);
        }

        private void SetTimestamp(string timestampTxt)
        {
            Timestamp = Math.Round(double.Parse(timestampTxt, CultureInfo.InvariantCulture.NumberFormat), 6);
        }
        
        private void SetEventValue(string eventValueTxt)
        {
            if ((EventType == "BTN_TOUCH") && (eventValueTxt == "UP"))
            {
                EventValue = TOUCH_UP;

            } else if ((EventType == "BTN_TOUCH") && (eventValueTxt == "DOWN"))
            {
                EventValue = TOUCH_DOWN;

            } else
            {
                EventValue = int.Parse(eventValueTxt, NumberStyles.HexNumber);
            }
        }

        public override string ToString()
        {
            string result = "";
            result += Timestamp + " ";
            result += Device + " ";
            result += OpCode + " ";
            result += EventType + " ";
            result += EventValue;

            return result;
        }
        
    }
}
