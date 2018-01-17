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
            string session = "17";

            string directory = "..\\..\\..\\Resources\\Raw\\" + session;

            string[] directories = { directory };

            Dataset aggregated = MergeDataset(directory);

            List<Dataset> datasets = new List<Dataset>();

            MergeDatasets(directories, datasets);

            string csv = ToCSV(datasets);

            System.IO.File.WriteAllText(session + ".csv", csv);
            */
            
            // BasePath
            string basePath = "..\\..\\..\\Resources\\Raw\\";

            string[] directories = Directory.GetDirectories(basePath);

            List<Dataset> datasets = new List<Dataset>();
            
            MergeDatasets(directories, datasets);

            SaveCSV(datasets, "dataset");

            //SaveJSON(datasets, "dataset");
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

        static void SaveCSV(List<Dataset> datasets, string datasetFile)
        {
            string csv = ToCSV(datasets);

            System.IO.File.WriteAllText(datasetFile + ".csv", csv);
        }

        static void SaveJSON(List<Dataset> datasets, string datasetFile)
        {
            string json = JsonConvert.SerializeObject(datasets, Formatting.Indented);

            System.IO.File.WriteAllText(datasetFile + ".json", datasetFile);
        }
    }
}
