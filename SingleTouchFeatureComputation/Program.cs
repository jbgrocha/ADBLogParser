﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleTouchSessionParser;
using Sessions;

namespace SingleTouchFeatureComputation
{
    class Program
    {
        public static void Main(string[] args)
        {
            PrintSessionToJSON("..\\..\\Resources\\Session-01.json");
        }

        private static void PrintSessionToJSON(string filePath)
        {
            SessionParser sessionParser = new SessionParser(filePath);
            Console.Write(sessionParser.Session.ToJSON());

        }
    }
}