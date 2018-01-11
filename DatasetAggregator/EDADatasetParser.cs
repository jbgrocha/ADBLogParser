using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class EDADatasetParser
    {
        public EDADataset Dataset;

        private string FilePath;
        private List<string> FileLines;

        public EDADatasetParser(string filePath)
        {
            Dataset = new EDADataset();

            FilePath = filePath;
            ReadFile();
            ParseDataset();
            NormalizeDatasetTime();
        }

        private void NormalizeDatasetTime()
        {
            double timeStamp = 0.0;
            double samplingRate = EDADataset.SamplingRate;

            for (int i = 0; i < Dataset.DataEntries.Count; i++)
            {
                if(i != 0)
                {
                    timeStamp = timeStamp + samplingRate;
                }

                Dataset.DataEntries[i].Timestamp = timeStamp;
            }
        }

        private void ParseDataset()
        {
            for(int i = 0; i < FileLines.Count; i++)
            {
                string line = FileLines.ElementAt(i);

                if (i != 0)
                {
                    Parse(line);
                }
            }
        }

        private void Parse(string line)
        {
            string[] splitLine = line.Split(';');

            //List<double> datasetEntry = new List<double>();

            EDADatasetEntry datasetEntry = new EDADatasetEntry
            {
                Timestamp = 0.0,

                EDA = double.Parse(splitLine.ElementAt(1), CultureInfo.InvariantCulture.NumberFormat)
            };

            Dataset.DataEntries.Add(datasetEntry);
        }

        private void ReadFile()
        {
            try
            {
                FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
