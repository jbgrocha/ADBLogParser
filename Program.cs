using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            ADBLogParser logParser = new ADBLogParser("..\\..\\Resources\\1.txt");
            //logParser.printFileLines();
            //logParser.printUnparsedEvents();
            //logParser.PrintParsedEvents();
            logParser.PrintFeatureSummary();

            /*
            // print feature summary for all sessions
            for (int i = 1; i < 18; i++ )
            {
                string file = i + ".txt";
                Console.WriteLine(file);
                ADBLogParser logParser = new ADBLogParser("..\\..\\Resources\\" + file);
                logParser.PrintFeatureSummary();
                Console.WriteLine();
            }
            */
        }
    }
}
