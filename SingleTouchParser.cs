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
        private List<Stroke> Strokes { get; set; }

        private Stroke CurrentStroke;
        private Sample CurrentSample;

        public SingleTouchParser(List<ADBLogEvent> EventsToParse)
        {
            Events = EventsToParse;
            Strokes = new List<Stroke>();

        }
    }
}
