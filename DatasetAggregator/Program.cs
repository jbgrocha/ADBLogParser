using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatasetAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            /*
            string session = "17";

            string directory = "..\\..\\..\\Resources\\Raw\\" + session;

            string[] directories = { directory };

            GenerateMergedCSV(directories, session);
            */

            // BasePath
            string basePath = "..\\..\\..\\Resources\\Raw\\";

            string[] directories = Directory.GetDirectories(basePath);

            GenerateCSVDataset(directories, "dataset");
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

        static void SaveCSVDataset(List<Dataset> datasets, string datasetFile)
        {
            string csv = ToCSV(datasets);

            System.IO.File.WriteAllText(datasetFile + ".csv", csv);
        }

        static void SaveJSONDataset(List<Dataset> datasets, string datasetFile)
        {
            string json = JsonConvert.SerializeObject(datasets, Formatting.Indented);

            System.IO.File.WriteAllText(datasetFile + ".json", datasetFile);
        }

        static void GenerateCSVDataset(string[] directories, string csvFile)
        {
            List<Dataset> datasets = new List<Dataset>();

            MergeDatasets(directories, datasets);

            SaveCSVDataset(datasets, csvFile);
        }

        static void GenerateJSONDataset(string[] directories, string jsonFile)
        {
            List<Dataset> datasets = new List<Dataset>();

            MergeDatasets(directories, datasets);

            SaveJSONDataset(datasets, jsonFile);
        }
    }
}
