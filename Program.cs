﻿using System;
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
            ADBLogParser logParser = new ADBLogParser("..\\..\\Resources\\StrokesLog1.txt");
            //logParser.printFileLines();
            logParser.printUnparsedEvents();
        }
    }
}
