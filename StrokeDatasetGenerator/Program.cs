using Newtonsoft.Json;
using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            string json = System.IO.File.ReadAllText("..\\..\\..\\Resources\\SingleTouch.json"); ;

            List<RawDataset> datasets = JsonConvert.DeserializeObject<List<RawDataset>>(json);

            StrokeParser parser = new StrokeParser(datasets);

            StrokeLabeller.Label(parser.Strokes);

            StrokeFeatureAdder.AddFeatures(parser.Strokes);

            foreach (Stroke stroke in parser.Strokes)
            {
                Console.WriteLine(stroke);
            }
            

            // read dataset - json List<RawDatasets> -> need to sort out the json reader (or a csv reader)
            // create StrokeParser, parses automatically
            // label dataset
            // add features to dataset
        }
    }
}
