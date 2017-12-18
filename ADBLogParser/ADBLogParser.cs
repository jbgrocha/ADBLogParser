using ADBLogParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using Strokes;
using System.Linq;

namespace LogParser
{
    class ADBLogParser
    {
        public Session Session;
        public List<ADBLogEvent> ParsedEvents { get; set; }

        public ADBLogParser(string filePath)
        {
            Session = new Session(filePath);
            ParseEvents();
            ParseStrokes();

        }

        private void ParseStrokes()
        {
            SingleTouchParser TouchParser = new SingleTouchParser(ParsedEvents);
            Session.Strokes = TouchParser.Strokes;
        }

        private void ParseEvents()
        {
            EventParser eventParser = new EventParser(Session.FilePath);
            ParsedEvents = eventParser.ParsedEvents;
        }

    }
}
