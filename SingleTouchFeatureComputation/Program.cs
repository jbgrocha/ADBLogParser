using Sessions;
using SingleTouchSessionParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleTouchFeatureComputation
{
    class Program
    {
        public static void Main(string[] args)
        {
            //PrintSessionToJSON("..\\..\\Resources\\Session-01.json");
            //List<double> test = Median("..\\..\\Resources\\Session-01.json", "ABS_MT_POSITION_X");

            Session session = ReadSession("..\\..\\Resources\\Session-01.json");

            List<double> test = Features.TimeElapsed(session);

            foreach (double value in test)
            {
                Console.WriteLine(value);
            }

        }

        //Generic operations
        private static void PrintSessionToJSON(string filePath)
        {
            Session session = ReadSession(filePath);
            Console.Write(session.ToJSON());
        }

        private static Session ReadSession(string filePath)
        {
            SessionParser sessionParser = new SessionParser(filePath);
            return sessionParser.Session;
        }

    }
}
