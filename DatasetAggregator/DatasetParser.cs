using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class DatasetParser
    {
        // Session

        // Touch Events Filepath
        private string TouchEventsFilepath;

        // Emotion Dataset Filepath
        private string EmotionDatasetFilepath;

        // EDA Dataset FilePath
        private string EDADatasetFilepath;


        // Touch Events
        public SampleDataset SampleDataset;

        // Emotion Dataset
        public VideoEmotionDataset EmotionDataset;

        // EDA Dataset
        public EDADataset EDADataset;

        public DatasetParser(string touchEventsFilepath, string emotionDatasetFilepath , string eDADatasetFilepath)
        {
            TouchEventsFilepath = touchEventsFilepath;
            EmotionDatasetFilepath = emotionDatasetFilepath;
            EDADatasetFilepath = eDADatasetFilepath;

            ParseTouchEvents();
            //Console.WriteLine(TouchEvents.ToString());
            
            ParseEmotionDataset();
            //Console.WriteLine(EmotionDataset.ToString());
            
            ParseEDADataset();
            //Console.WriteLine(EDADataset.ToString());

        }

        private void ParseEDADataset()
        {
            if (EDADatasetFilepath != null)
            {
                EDADatasetParser parser = new EDADatasetParser(EDADatasetFilepath);
                EDADataset = parser.Dataset;
            }
            else
            {
                EDADataset = new EDADataset();
            }
            
        }

        private void ParseEmotionDataset()
        {
            if(EmotionDatasetFilepath != null)
            {
                VideoEmotionDatasetParser parser = new VideoEmotionDatasetParser(EmotionDatasetFilepath);
                EmotionDataset = parser.Dataset;
            }
            else
            {
                EmotionDataset = new VideoEmotionDataset();
            }
            
        }

        private void ParseTouchEvents()
        {
            ADBLogEventsParser parser = new ADBLogEventsParser(TouchEventsFilepath);
            SampleParser sampleParser = new SampleParser(parser.Dataset);

            SampleDataset = sampleParser.Dataset;
        }
    }
}
