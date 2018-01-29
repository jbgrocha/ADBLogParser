using Newtonsoft.Json;
using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;
using Accord.MachineLearning.DecisionTrees.Learning;

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
                // NOTE: Strokes that are not labeled with an emotion are skipped
                if ((stroke.Emotion != null) || (stroke.Emotion != ""))
                {
                    classificationLabel.Add(labels[stroke.Emotion]);

                    List<double> strokeFeatures = new List<double>();

                    // In every stroke the features were added in order so no need to worry too much about this
                    // There should be no null features
                    // The features that were previously added are (in order) : length, mean speed, directness, mean contact area
                    // this means that there is always the same features for every stroke

                    // TODO : fix features in the feature computation, don't know why it is a double? when it starts out as a simple double

                    foreach (KeyValuePair<string, double?> feature in stroke.Features)
                    {
                        strokeFeatures.Add((double)feature.Value);
                    }

                    features.Add(strokeFeatures.ToArray());
                }
            }

            int[] _classificationLabel = classificationLabel.ToArray();

            double[][] _features = features.ToArray();

            // Random Forest
            // Ensure we have reproducible results
            Accord.Math.Random.Generator.Seed = 0;

            // random forest
            var cv = CrossValidation.Create(

                k: 10, // We will be using 10-fold cross validation

                learner: (p) => new RandomForestLearning()
                {
                    NumberOfTrees = 10, // use 10 trees in the forest
                },

                // Now we have to specify how the tree performance should be measured:
                loss: (actual, expected, p) => new ZeroOneLoss(expected).Loss(actual),

                // This function can be used to perform any special
                // operations before the actual learning is done, but
                // here we will just leave it as simple as it can be:
                fit: (teacher, x, y, w) => teacher.Learn(x, y, w),

                // Finally, we have to pass the input and output data
                // that will be used in cross-validation. 
                x: _features, y: _classificationLabel
            );


            // Decision tree c45
            //var cv = CrossValidation.Create(

            //    k: 10, // We will be using 10-fold cross validation

            //    learner: (p) => new C45Learning() // here we create the learning algorithm
            //    {
            //        Join = 2,
            //        MaxHeight = 5
            //    },

            //    // Now we have to specify how the tree performance should be measured:
            //    loss: (actual, expected, p) => new ZeroOneLoss(expected).Loss(actual),

            //    // This function can be used to perform any special
            //    // operations before the actual learning is done, but
            //    // here we will just leave it as simple as it can be:
            //    fit: (teacher, x, y, w) => teacher.Learn(x, y, w),

            //    // Finally, we have to pass the input and output data
            //    // that will be used in cross-validation. 
            //    x: _features, y: _classificationLabel
            //);


            // After the cross-validation object has been created,
            // we can call its .Learn method with the input and 
            // output data that will be partitioned into the folds:
            var result = cv.Learn(_features, _classificationLabel);

            // We can grab some information about the problem:
            int numberOfSamples = result.NumberOfSamples;
            int numberOfInputs = result.NumberOfInputs;
            int numberOfOutputs = result.NumberOfOutputs;

            double trainingError = result.Training.Mean;
            double validationError = result.Validation.Mean;

            // If desired, compute an aggregate confusion matrix for the validation sets:
            GeneralConfusionMatrix gcm = result.ToConfusionMatrix(_features, _classificationLabel);
            double accuracy = gcm.Accuracy;


            Console.WriteLine("Number of samples: " + numberOfSamples);
            Console.WriteLine("Number of inputs: " + numberOfInputs);
            Console.WriteLine("Number of outputs: " + numberOfOutputs);

            Console.WriteLine("Training error: " + trainingError);
            Console.WriteLine("Validation Error: " + validationError);

            Console.WriteLine("Accuracy: " + accuracy);

        }
    }
}
