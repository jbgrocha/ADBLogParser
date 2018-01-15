using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatasetAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            // BasePath
            string basePath = "..\\..\\..\\Resources\\Raw\\";

            string[] directories = Directory.GetDirectories(basePath);

            List<Dataset> datasets = new List<Dataset>();
            
            MergeDatasets(directories, datasets);

            string jsonDataset = JsonConvert.SerializeObject(datasets, Formatting.Indented);

            Console.WriteLine(jsonDataset);
        }

        static void MergeDatasets(string[] directories, List<Dataset> datasets)
        {
            foreach (string directory in directories)
            {
                Dataset aggregated = MergeDataset(directory);
                datasets.Add(aggregated);
            }
        }

        static Dataset MergeDataset(string directory)
        {
            // Touch Events
            string touchFilepath = directory + "\\Strokes.txt";

            // Emotion Events
            string emotionFilepath = directory + "\\Video.csv";

            // EDA Events
            string edaFilepath = directory + "\\EDA.csv";

            DatasetParser parser = new DatasetParser(touchFilepath, emotionFilepath, edaFilepath);

            DatasetAggregator aggregator = new DatasetAggregator(directory, parser.TouchEvents, parser.EmotionDataset, parser.EDADataset);

            return aggregator.Dataset;
        }
    }
}
