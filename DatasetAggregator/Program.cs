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

            /*
            ADBLogEventsParser eventsParser = new ADBLogEventsParser("..\\..\\..\\Resources\\Raw\\05\\Strokes.txt");

            SampleParser sampleParser = new SampleParser(eventsParser.Dataset);
            
            Console.WriteLine(Sample.Headers);

            Console.WriteLine(sampleParser.Dataset.ToString());
            */
            /*
            string directory = "..\\..\\..\\Resources\\Raw\\01";

            Dataset aggregated = MergeDataset(directory);

            Console.WriteLine(aggregated.ToString());
            */
            
            // BasePath
            string basePath = "..\\..\\..\\Resources\\Raw\\";

            string[] directories = Directory.GetDirectories(basePath);

            List<Dataset> datasets = new List<Dataset>();
            
            MergeDatasets(directories, datasets);

            string csv = ToCSV(datasets);

            System.IO.File.WriteAllText(@"dataset.csv", csv);

            //Console.WriteLine(ToCSV(datasets));

            //string jsonDataset = JsonConvert.SerializeObject(datasets, Formatting.Indented);

            //Console.WriteLine(jsonDataset);
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

            DatasetAggregator aggregator = new DatasetAggregator(directory, parser.SampleDataset, parser.EmotionDataset, parser.EDADataset);

            return aggregator.Dataset;
        }

        static string ToCSV(List<Dataset> datasets)
        {
            string result = Dataset.CSVHeaders + "\n";

            foreach(Dataset dataset in datasets)
            {
                result += dataset.ToString();
            }

            return result;
        }
    }
}
