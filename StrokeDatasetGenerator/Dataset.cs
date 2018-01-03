using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    public class Dataset
    {
        public List<string> Labels { get; set; }
        public List<List<double>> Features { get; set; }

        public Dataset()
        {
            Labels = new List<string>();
            Features = new List<List<double>>();
        }

        public override string ToString()
        {
            string result = "";

            result += LabelsToString();
            result += FeaturesToString();

            return result;
        }

        private string FeaturesToString()
        {
            string result = "";

            foreach (List<double> stroke in Features)
            {
                result += StrokeFeaturesToString(stroke);
                result += "\n";
            }

            return result;
        }

        private string StrokeFeaturesToString(List<double> strokeFeatures)
        {
            string result = "";

            for(int i = 0; i < strokeFeatures.Count; i++)
            {
                result += strokeFeatures.ElementAt(i);

                if (i != strokeFeatures.Count - 1) {
                    result += ";";
                }
            }

            return result;
        }

        private string LabelsToString()
        {
            string result = "";

            for (int i = 0; i < Labels.Count; i++)
            {
                result += Labels.ElementAt(i);

                if (i != Labels.Count - 1)
                {
                    result += ";";
                }
            }

            return result;
        }
    }
}
