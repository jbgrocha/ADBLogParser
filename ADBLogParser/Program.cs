using System;
using System.IO;
using ADBLogParser;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrintFeatureEventSummaryAllSessions();
            //PrintFeatureEventSummarySingleTouchSessions();
            //PrintFeatureEventSummary("..\\..\\Resources\\SingleTouch\\01.txt");
            //PrintFeatureSummaryToJSON("..\\..\\Resources\\SingleTouch\\01.txt");


            //PrintTouchEventSummaryAllSessions();
            //PrintTouchEventSummarySingleTouchSessions();

            //PrintStrokes("..\\..\\Resources\\SingleTouch\\01.txt");

            //PrintStrokesToJSON("..\\..\\Resources\\SingleTouch\\01.txt");
            //PrintSessionToJSON("..\\..\\Resources\\SingleTouch\\01.txt");
            PrintSessionValidation("..\\..\\Resources\\SingleTouch\\01.txt");
        }

        private static void PrintFeatureEventSummarySingleTouchSessions()
        {
            string targetDirectory = "..\\..\\Resources\\SingleTouch\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Feature Summary - Single Touch Sessions");
            Console.WriteLine();

            PrintFeatureEventSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintFeatureEventSummaryAllSessions()
        {
            string targetDirectory = "..\\..\\Resources\\All\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Feature Summary - All Sessions");
            Console.WriteLine();

            PrintFeatureEventSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintTouchEventSummarySingleTouchSessions()
        {
            string targetDirectory = "..\\..\\Resources\\SingleTouch\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Touch Summary - Single Touch Sessions");
            Console.WriteLine();

            PrintTouchEventSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintTouchEventSummaryAllSessions()
        {
            string targetDirectory = "..\\..\\Resources\\All\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Touch Summary - All Touch Sessions");
            Console.WriteLine();

            PrintTouchEventSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintFeatureEventSummaries(string[] fileEntries)
        {
            foreach (string fileEntry in fileEntries)
            {
                PrintFeatureEventSummary(fileEntry);
            }
        }

        private static void PrintTouchEventSummaries(string[] fileEntries)
        {
            foreach (string fileEntry in fileEntries)
            {
                PrintTouchEventSummary(fileEntry);
            }
        }
        
        private static void PrintFeatureEventSummary(string filePath)
        {
            Console.WriteLine(filePath);
            EventParser eventParser = new EventParser(filePath);
            eventParser.PrintFeatureEventSummary();
            Console.WriteLine();
        }

        private static void PrintTouchEventSummary(string filePath)
        {
            Console.WriteLine(filePath);
            EventParser eventParser = new EventParser(filePath);
            eventParser.PrintTouchEventSummary();
            Console.WriteLine();
        }

        private static void PrintStrokes(string filePath)
        {
            Console.WriteLine(filePath);
            ADBLogParser logParser = new ADBLogParser(filePath);
            logParser.Session.PrintStrokes();
        }

        private static void PrintFeatureSummaryToJSON(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.Session.FeatureSummaryToJSON());
        }

        private static void PrintStrokesToJSON(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.Session.StrokesToJSON());
        }

        private static void PrintSessionToJSON(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.Session.ToJSON());
        }

        private static void PrintSessionValidation(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.Session.ValidateSession());
        }
    }
}
