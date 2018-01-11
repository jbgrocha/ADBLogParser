using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            // BasePath
            string basePath = "..\\..\\Resources\\";

            // Touch Events
            string touchFilepath = basePath + "01-Strokes.txt";

            // Emotion Events
            string emotionFilepath = null;//basePath + "01-Emotions.csv";

            // EDA Events
            string edaFilepath = basePath + "01-EDA.csv";

            DatasetParser parser = new DatasetParser(touchFilepath, emotionFilepath, edaFilepath);

            DatasetAggregator aggregator = new DatasetAggregator(1, parser.TouchEvents, parser.EmotionDataset, parser.EDADataset);

            //Console.WriteLine(aggregator.Dataset.Entries.Count);
            Console.Write(aggregator.Dataset.ToJSON());
            //Console.Write(aggregator.Dataset.ToString());
        }
    }
}
