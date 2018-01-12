using System;
using System.Collections.Generic;
using System.IO;
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
            string basePath = "..\\..\\..\\Resources\\";

            string[] directories = Directory.GetDirectories(basePath);

            foreach(string directory in directories) {
                // Touch Events
                string touchFilepath = directory + "\\Strokes.txt";

                // Emotion Events
                string emotionFilepath = directory + "\\Video.csv";

                // EDA Events
                string edaFilepath = directory + "\\EDA.csv";

                DatasetParser parser = new DatasetParser(touchFilepath, emotionFilepath, edaFilepath);

                DatasetAggregator aggregator = new DatasetAggregator(1, parser.TouchEvents, parser.EmotionDataset, parser.EDADataset);

                //Console.WriteLine(emotionFilepath);
                Console.Write(aggregator.Dataset.ToJSON());
            }

            //Console.WriteLine(aggregator.Dataset.Entries.Count);
            //Console.Write(aggregator.Dataset.ToJSON());
            //Console.Write(aggregator.Dataset.ToString());
        }
    }
}
