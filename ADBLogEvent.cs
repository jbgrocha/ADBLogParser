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
         * @jbgrocha: I'm suspicious of this 'timestamp' implementation 
         * I suspect issues may occur when I attempt to synchronize the
         * emotion data or the EDA data with this
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
            this.SetTimestamp(eventText[0]);

            this.Device = eventText[1];

            this.OpCode = eventText[2];

            this.EventType = eventText[3];
            
            this.SetEventValue(eventText[4]);
        }

        private void SetTimestamp(string timestampTxt)
        {
            this.Timestamp = Math.Round(double.Parse(timestampTxt, CultureInfo.InvariantCulture.NumberFormat), 6);
        }
        
        private void SetEventValue(string eventValueTxt)
        {
            if ((this.EventType == "BTN_TOUCH") && (eventValueTxt == "UP"))
            {
                this.EventValue = TOUCH_UP;

            } else if ((this.EventType == "BTN_TOUCH") && (eventValueTxt == "DOWN"))
            {
                this.EventValue = TOUCH_DOWN;

            } else
            {
                this.EventValue = int.Parse(eventValueTxt, NumberStyles.HexNumber);
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
