using Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    public class DatasetGenerator
    {
        private Session Session;
        public Dataset Dataset;

        public DatasetGenerator(Session session)
        {
            Session = session;
            GenerateDataset();

            Console.Write(Dataset);
        }

        private void GenerateDataset()
        {
            Dataset = new Dataset();

            SetLabels();
            SetFeatures();
        }

        private void SetFeatures()
        {
            foreach (Stroke stroke in Session.Strokes)
            {
                List<double> strokeFeatures = stroke.Features.Values.ToList();
                Dataset.Features.Add(strokeFeatures);
            }
        }

        private void SetLabels()
        {

        }
    }
}
