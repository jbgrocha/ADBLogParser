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

            Features.Mean_X(session);
            Features.Max_X(session);
            Features.Min_X(session);
            Features.StandardDeviation_X(session);


            Features.Mean_Y(session);
            Features.Max_Y(session);
            Features.Min_Y(session);
            Features.StandardDeviation_Y(session);

            Features.Duration(session);
            Features.Dist2Prev(session);
            Features.TimeElapsed(session);

            PrintSessionToJSON(session);

        }

        //Helpers
        private static void PrintSessionToJSON(Session session)
        {
            Console.Write(session.ToJSON());
        }

        private static Session ReadSession(string filePath)
        {
            SessionParser sessionParser = new SessionParser(filePath);
            return sessionParser.Session;
        }

    }
}
