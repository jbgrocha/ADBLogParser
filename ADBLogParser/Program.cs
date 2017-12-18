using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //ADBLogParser logParser = new ADBLogParser("..\\..\\Resources\\SingleTouch\\01.txt");

            //logParser.printFileLines();
            //logParser.printUnparsedEvents();
            //logParser.PrintParsedEvents();
            //logParser.PrintFeatureSummary();
            //logParser.PrintTouchSummary();

            //PrintFeatureSummaryAllSessions();
            //PrintFeatureSummarySingleTouchSessions();
            //PrintFeatureSummary("..\\..\\Resources\\SingleTouch\\01.txt");
            //PrintFeatureSummaryJSON("..\\..\\Resources\\SingleTouch\\01.txt");


            //PrintTouchSummaryAllSessions();
            //PrintTouchSummarySingleTouchSessions();

            //PrintStrokes("..\\..\\Resources\\SingleTouch\\01.txt");
            PrintJSONStrokes("..\\..\\Resources\\SingleTouch\\01.txt");
        }

        private static void PrintFeatureSummaryJSON(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.FeatureSummaryToJSON());
        }

        /*
        private static void PrintFeatureSummarySingleTouchSessions()
        {
            string targetDirectory = "..\\..\\Resources\\SingleTouch\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Feature Summary - Single Touch Sessions");
            Console.WriteLine();

            PrintFeatureSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintFeatureSummaryAllSessions()
        {
            string targetDirectory = "..\\..\\Resources\\All\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Feature Summary - All Sessions");
            Console.WriteLine();

            PrintFeatureSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintTouchSummarySingleTouchSessions()
        {
            string targetDirectory = "..\\..\\Resources\\SingleTouch\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Touch Summary - Single Touch Sessions");
            Console.WriteLine();

            PrintTouchSummaries(fileEntries);

            Console.WriteLine();
        }

        private static void PrintTouchSummaryAllSessions()
        {
            string targetDirectory = "..\\..\\Resources\\All\\";
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            Console.WriteLine("Touch Summary - All Touch Sessions");
            Console.WriteLine();

            PrintTouchSummaries(fileEntries);

            Console.WriteLine();
        }
        /*
        private static void PrintFeatureSummaries(string[] fileEntries)
        {
            foreach (string fileEntry in fileEntries)
            {
                PrintFeatureSummary(fileEntry);
            }
        }

        private static void PrintTouchSummaries(string[] fileEntries)
        {
            foreach (string fileEntry in fileEntries)
            {
                PrintTouchSummary(fileEntry);
            }
        }
        
        
        private static void PrintFeatureSummary(string filePath)
        {
            Console.WriteLine(filePath);
            ADBLogParser logParser = new ADBLogParser(filePath);
            logParser.PrintFeatureSummary();
            Console.WriteLine();
        }

        private static void PrintTouchSummary(string filePath)
        {
            Console.WriteLine(filePath);
            ADBLogParser logParser = new ADBLogParser(filePath);
            logParser.PrintTouchSummary();
            Console.WriteLine();
        }
        */

        private static void PrintStrokes(string filePath)
        {
            Console.WriteLine(filePath);
            ADBLogParser logParser = new ADBLogParser(filePath);
            logParser.PrintStrokes();
        }

        private static void PrintJSONStrokes(string filePath)
        {
            ADBLogParser logParser = new ADBLogParser(filePath);
            Console.Write(logParser.StrokesToJSON());
        }
    }
}
