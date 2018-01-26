using RawDatasetGenerator;
using StrokeDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    // May need to change to not be static
    public static class StrokeFeatureAdder
    {
        public static void AddFeatures(List<Stroke> strokes)
        {
            foreach (Stroke stroke in strokes)
            {
                // get base feature arrays for the stroke
                FeatureComputation featureComputation = new FeatureComputation(stroke);

                // compute auxiliary arrays?

                // compute features
                featureComputation.ComputeFeatures();
                stroke.Features = featureComputation.Features;
            }
        }
    }    
}
