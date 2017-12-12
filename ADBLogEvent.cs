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
        public double timestamp { get; set; }
        public string device { get; set; }
        public string opCode { get; set; }
        public string eventType { get; set; }
        public int eventValue { get; set; }

        public const int TOUCH_UP = 0;
        public const int TOUCH_DOWN = 1;

        public ADBLogEvent(string[] eventText)
        {
            this.setTimestamp(eventText[0]);

            this.device = eventText[1];

            this.opCode = eventText[2];

            this.eventType = eventText[3];
            
            this.setEventValue(eventText[4]);
        }

        private void setTimestamp(string timestampTxt)
        {
            this.timestamp = Math.Round(double.Parse(timestampTxt, CultureInfo.InvariantCulture.NumberFormat), 6);
        }
        
        private void setEventValue(string eventValueTxt)
        {
            if ((this.eventType == "BTN_TOUCH") && (eventValueTxt == "UP"))
            {
                this.eventValue = TOUCH_UP;

            } else if ((this.eventType == "BTN_TOUCH") && (eventValueTxt == "DOWN"))
            {
                this.eventValue = TOUCH_DOWN;

            } else
            {
                this.eventValue = int.Parse(eventValueTxt, NumberStyles.HexNumber);
            }
        }

        
        public override string ToString()
        {
            string result = "";
            result += timestamp + " ";
            result += device + " ";
            result += opCode + " ";
            result += eventType + " ";
            result += eventValue;

            return result;
        }
        
    }
}
