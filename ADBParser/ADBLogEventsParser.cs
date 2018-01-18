using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;


namespace ADBParser
{
    public class ADBLogEventsParser
    {

        private string FilePath { get; set; }
        private List<string> FileLines { get; set; }
        private List<string[]> UnparsedEvents { get; set; }

        public ADBTouchEventsDataset Dataset { get; set; }

        public ADBLogEventsParser(string filePath)
        {
            FilePath = filePath;

            if(File.Exists(FilePath))
            {
                ReadFile();
                DiscardLines();
                CleanUpLines();
                ReadUnparsedEvents();
                ParseLogEvents();
                NormalizeDatasetTime();
            }
        }

        private void NormalizeDatasetTime()
        {
            double initialTimestamp = 0.0;

            for (int i = 0; i < Dataset.DataEntries.Count; i++)
            {
                if (i == 0)
                {
                    initialTimestamp = Dataset.DataEntries[i].Timestamp;
                }

                Dataset.DataEntries[i].Timestamp = (Dataset.DataEntries[i].Timestamp - initialTimestamp);
            }
        }

        private void ParseLogEvents()
        {
            Dataset = new ADBTouchEventsDataset();

            foreach (string[] unparsedEvent in UnparsedEvents)
            {
                ADBLogEvent parsedEvent = ParseADBLogEvent(unparsedEvent);
                Dataset.DataEntries.Add(parsedEvent);
            }
        }

        private ADBLogEvent ParseADBLogEvent(string[] unparsedEvent)
        {
            double timestamp = double.Parse(unparsedEvent[0], CultureInfo.InvariantCulture.NumberFormat) * 1000; // logs keeep seconds and not milliseconds

            string device = unparsedEvent[1];

            string opCode = unparsedEvent[2];

            string eventType = unparsedEvent[3];

            int value = GetEventValue(eventType, unparsedEvent[4]);

            ADBLogEvent result = new ADBLogEvent(timestamp, device, opCode, eventType, value);

            return result;
        }

        private int GetEventValue(string eventType, string eventValueTxt)
        {
            int eventValue = 0;

            if ((eventType == "BTN_TOUCH") && (eventValueTxt == "UP"))
            {
                eventValue = ADBLogEvent.TOUCH_UP;

            }
            else if ((eventType == "BTN_TOUCH") && (eventValueTxt == "DOWN"))
            {
                eventValue = ADBLogEvent.TOUCH_DOWN;

            }
            else
            {
                eventValue = int.Parse(eventValueTxt, NumberStyles.HexNumber);
            }

            return eventValue;
        }

        private void ReadUnparsedEvents()
        {
            UnparsedEvents = new List<string[]>();

            foreach (string line in FileLines)
            {
                string[] unparsedEvent = ReadUnparsedEvent(line);
                UnparsedEvents.Add(unparsedEvent);
            }
        }

        private string[] ReadUnparsedEvent(string line)
        {
            string[] result = line.Split(' ');
            return result;
        }

        public void PrintUnparsedEvents()
        {
            foreach (string[] unparsedEvent in UnparsedEvents)
            {
                PrintUnparsedEvent(unparsedEvent);
            }
        }

        private void PrintUnparsedEvent(string[] unparsedEvent)
        {
            foreach (string property in unparsedEvent)
            {
                Console.Out.Write(property + " ");
            }
            Console.Out.WriteLine();
        }

        private void CleanUpLines()
        {
            for (int i = 0; i < FileLines.Count; i++)
            {
                FileLines[i] = CleanUpLine(FileLines[i]);
            }
        }

        private string CleanUpLine(string line)
        {
            string result = line;

            result = result.Replace("[", "").Replace("]", "");
            result = result.Trim();
            result = Regex.Replace(result, @"\s+", " ");

            return result;
        }

        private void DiscardLines()
        {
            List<string> cleanedFileLines = new List<string>();

            for (int i = 0; i < FileLines.Count; i++)
            {
                string str = FileLines[i];

                if (KeepLine(str))
                {
                    cleanedFileLines.Add(str);
                }
            }

            FileLines = cleanedFileLines;
        }

        private bool KeepLine(string str)
        {
            bool result = str.StartsWith("[");

            return result;
        }

        public void PrintFileLines()
        {
            foreach (string line in FileLines)
            {
                Console.Out.WriteLine(line);
            }
        }

        private void ReadFile()
        {
            try
            {
                FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                //Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
