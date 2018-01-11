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
            // Parse Touch Events
            // Emotion Events
            // EDA Events
            DatasetParser parser = new DatasetParser("..\\..\\Resources\\01-Strokes.txt", "..\\..\\Resources\\01-Emotions.csv", "..\\..\\Resources\\01-EDA.csv");

            DatasetAggregator aggregator = new DatasetAggregator(1, parser.TouchEvents, parser.EmotionDataset, parser.EDADataset);

            Console.Write(aggregator.Dataset.ToJSON());
            //Console.Write(aggregator.Dataset.ToString());
        }
    }
}
