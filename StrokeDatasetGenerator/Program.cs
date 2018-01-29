using Newtonsoft.Json;
using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accord.MachineLearning.DecisionTrees;
using Accord.Math.Optimization.Losses;

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

            /*
            foreach (Stroke stroke in parser.Strokes)
            {
                Console.WriteLine(stroke);
            }
            */

            List<double[]> features = new List<double[]>();

            List<int> classificationLabel = new List<int>();

            // not currently used
            Dictionary<string, int> labels = new Dictionary<string, int>() {
                {"", 0 },
                { "Neutral", 1 },
                {"Happy", 2},
                {"Sad", 3},
                {"Angry", 4},
                {"Surprised", 5},
                {"Scared", 6},
                {"Disgusted", 7},
                {"Contempt", 8}
            };

            foreach(Stroke stroke in parser.Strokes)
            {
                classificationLabel.Add( labels[ stroke.Emotion ] );
                //Console.WriteLine(stroke.Emotion);
                
                // NOTE: Strokes that are not labeled with an emotion should probably be skipped

                List<double> strokeFeatures = new List<double>();

                // In every stroke the features were added in order so no need to worry too much about this
                // There should be no null features
                // The features that were previously added are (in order) : length, mean speed, directness, mean contact area
                // this means that there is always the same features for every stroke
                // TODO : fix features in the feature computation, don't know why it is a double? when it starts out as a simple double

                foreach (KeyValuePair<string, double?> feature in stroke.Features)
                {
                    strokeFeatures.Add( (double) feature.Value);
                }

                features.Add(strokeFeatures.ToArray());

            }

            int[] _classificationLabel = classificationLabel.ToArray();

            double[][] _features = features.ToArray();


            // just some debugs printouts
            /*
            for(int i = 0; i < _classificationLabel.Length; i++)
            {
                Console.WriteLine(_classificationLabel[i]);
            }
            */

            //Console.WriteLine(_classificationLabel.Length);

            /*
            for(int i = 0; i < _features.Length; i++)
            {
                double[] strokeFeatures = _features[i];

                string featureStr = "";

                for(int j = 0; j < strokeFeatures.Length; j++)
                {
                    featureStr += strokeFeatures[j];
                    featureStr += ";";
                }

                Console.WriteLine(featureStr);
            }
            */




            // training and testing
            // Create the forest learning algorithm

            // Fix random seed for reproducibility
            Accord.Math.Random.Generator.Seed = 1;

            var teacher = new RandomForestLearning()
            {
                NumberOfTrees = 10, // use 10 trees in the forest
            };

            // Finally, learn a random forest from data
            var forest = teacher.Learn(_features, _classificationLabel);

            // We can estimate class labels using
            int[] predicted = forest.Decide(_features);

            // And the classification error (0.0006) can be computed as 
            double error = new ZeroOneLoss(_classificationLabel).Loss(forest.Decide(_features));

            Console.WriteLine("Zero One Loss: " + error);


            //Console.WriteLine(_features.Length);

            // convert dataset features to double [][]

            // convert dataset labels to int[]

            // read dataset - json List<RawDatasets> -> need to sort out the json reader (or a csv reader)
            // create StrokeParser, parses automatically
            // label dataset
            // add features to dataset
        }
    }
}
