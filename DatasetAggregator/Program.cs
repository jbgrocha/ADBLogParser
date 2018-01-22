using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RawDatasetGenerator
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
            //GenerateJSONDataset(directories, "dataset");
        }

        static void MergeDatasets(string[] directories, List<RawDataset> datasets)
        {
            foreach (string directory in directories)
            {
                RawDataset aggregated = MergeDataset(directory);
                datasets.Add(aggregated);
            }
        }

        static RawDataset MergeDataset(string directory)
        {
            // Touch Events
            string touchFilepath = directory + "\\Strokes.txt";

            // Emotion Events
            string emotionFilepath = directory + "\\Video.csv";

            // EDA Events
            string edaFilepath = directory + "\\EDA.csv";

            RawDatasetParser parser = new RawDatasetParser(touchFilepath, emotionFilepath, edaFilepath);

            RawDatasetGenerator aggregator = new RawDatasetGenerator(directory, parser.SampleDataset, parser.EmotionDataset, parser.EDADataset);

            return aggregator.Dataset;
        }

        static string ToCSV(List<RawDataset> datasets)
        {
            string result = RawDataset.CSVHeaders + "\n";

            foreach(RawDataset dataset in datasets)
            {
                result += dataset.ToString();
            }

            return result;
        }

        static void SaveCSVDataset(List<RawDataset> datasets, string datasetFile)
        {
            string csv = ToCSV(datasets);

            System.IO.File.WriteAllText(datasetFile + ".csv", csv);
        }

        static void SaveJSONDataset(List<RawDataset> datasets, string datasetFile)
        {
            string json = JsonConvert.SerializeObject(datasets, Formatting.Indented);

            System.IO.File.WriteAllText(datasetFile + ".json", json);
        }

        static void GenerateCSVDataset(string[] directories, string csvFile)
        {
            List<RawDataset> datasets = GenerateDatasets(directories);

            SaveCSVDataset(datasets, csvFile);
        }

        static void GenerateJSONDataset(string[] directories, string jsonFile)
        {
            List<RawDataset> datasets = GenerateDatasets(directories);

            SaveJSONDataset(datasets, jsonFile);
        }

        static List<RawDataset> GenerateDatasets(string[] directories)
        {
            List<RawDataset> datasets = new List<RawDataset>();

            MergeDatasets(directories, datasets);

            return datasets;
        }
    }
}
