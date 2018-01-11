﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class EDADatasetEntry
    {
        public double Timestamp { get; set;}

        public double EDA { get; set; }

        public EDADatasetEntry()
        {
            Timestamp = 0.0;
            EDA = 0.0;
        }

        public EDADatasetEntry(double timestamp, double eda)
        {
            Timestamp = timestamp;
            EDA = eda;
        }

        public override string ToString()
        {
            string result = Timestamp.ToString();

            result += ";" + EDA.ToString();

            return result;
        }
    }
}
