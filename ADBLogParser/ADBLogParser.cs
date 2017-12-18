using ADBLogParser;
using System.Collections.Generic;
using Sessions;

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
            SingleTouchParser touchParser = new SingleTouchParser(ParsedEvents, Session);
            Session = touchParser.Session;
        }

        private void ParseEvents()
        {
            EventParser eventParser = new EventParser(Session.FilePath);
            ParsedEvents = eventParser.ParsedEvents;
        }

    }
}
