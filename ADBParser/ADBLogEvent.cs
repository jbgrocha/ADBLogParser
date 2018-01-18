using System;
using System.Globalization;

namespace ADBParser
{
    public class ADBLogEvent
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

        public ADBLogEvent(double timestamp, string device, string opCode, string eventType, int eventValue)
        {
            Timestamp = timestamp;
            Device = device;
            OpCode = opCode;
            EventType = eventType;
            EventValue = eventValue;
        }

        public override string ToString()
        {
            string result = "";
            result += Timestamp + ";";
            result += Device + ";";
            result += OpCode + ";";
            result += EventType + ";";
            result += EventValue;

            return result;
        }
        
    }
}
