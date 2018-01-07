using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEmotionDatasetParser
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoEmotionDataset dataset = ReadDataset("..\\..\\Resources\\01.csv");

            Console.Write(dataset.ToString());
        }

        private static VideoEmotionDataset ReadDataset(string filePath)
        {
            VideoEmotionDatasetParser datasetParser = new VideoEmotionDatasetParser(filePath);
            return datasetParser.Dataset;
        }
    }
}
