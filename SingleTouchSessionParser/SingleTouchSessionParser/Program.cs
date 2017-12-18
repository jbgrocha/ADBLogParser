using Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTouchSessionParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            PrintSessionToJSON("..\\..\\Resources\\Session-01.json");
        }

        private static void PrintSessionToJSON(string filePath)
        {
            SingleTouchSessionParser sessionParser = new SingleTouchSessionParser(filePath);
            Console.Write(sessionParser.Session.ToJSON());

        }

    }
}
