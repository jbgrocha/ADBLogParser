using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADBParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = "..\\..\\..\\Resources\\Raw\\";
            string[] sessions = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17" };

            foreach (string session in sessions)
            {
                string TouchEventsFilepath = basePath + session + "\\Strokes.txt";

                ADBLogEventsParser parser = new ADBLogEventsParser(TouchEventsFilepath);

                Console.WriteLine("Session: " + session);
                Console.WriteLine(parser.Dataset.ValidateSync());
            }
        }
    }
}
