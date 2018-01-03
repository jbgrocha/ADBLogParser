using Sessions;
using SingleTouchSessionParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Session session = ReadSession("..\\..\\Resources\\Session-01.json");
            DatasetGenerator datasetGenerator = new DatasetGenerator(session);
            
            //PrintSessionToJSON(session);
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
