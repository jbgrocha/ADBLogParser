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

            if (File.Exists(FilePath))
            {
                ReadFile();
                ParseDataset();
            }
        }

        private void ParseDataset()
        {
            for(int i = 0; i < FileLines.Count; i++)
            {
                string line = FileLines.ElementAt(i);

                if (i != 0)
                {
                    Parse(line, i);
                }
            }
        }

        private void Parse(string line, int lineNumber)
        {
            string[] splitLine = line.Split(';');

            EDADatasetEntry datasetEntry = new EDADatasetEntry
            {
                Timestamp = (lineNumber - 1) * EDADataset.SamplingRate,

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
                Console.WriteLine(e.ToString());
            }
        }
    }
}
