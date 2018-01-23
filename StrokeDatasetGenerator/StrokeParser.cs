using RawDatasetGenerator;
using SampleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    public class StrokeParser
    {
        public List<RawDataset> Datasets {get; set; }
        public List<Stroke> Strokes { get; }

        public StrokeParser(List<RawDataset> datasets)
        {
            Datasets = datasets;
            Strokes = new List<Stroke>();
            Parse();
        }

        public void Parse()
        {
            foreach(RawDataset dataset in Datasets)
            {
                Parse(dataset);
            }
        }

        private void Parse(RawDataset dataset)
        {
            Stroke currentStroke = null;

            foreach(RawDatasetEntry entry in dataset.Entries)
            {
                if(entry.TouchSample.ButtonTouch == Sample.TOUCH_DOWN)
                {
                    currentStroke = new Stroke();

                    currentStroke.RawDatasetEntries.Add(entry);
                }
                else if(entry.TouchSample.ButtonTouch == Sample.TOUCH_UP)
                {
                    currentStroke.RawDatasetEntries.Add(entry);

                    Strokes.Add(currentStroke);

                    currentStroke = null;
                }
                else
                {
                    currentStroke.RawDatasetEntries.Add(entry);
                }
            }
        }
    }
}
