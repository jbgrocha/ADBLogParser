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
        private string TouchEventsFilepath = "..\\..\\Resources\\01-Strokes.txt";

        // Emotion Dataset Filepath
        private string EmotionDatasetFilepath = "..\\..\\Resources\\01-Emotions.csv";

        // EDA Dataset FilePath
        private string EDADatasetFilepath = "";


        // Touch Events
        public ADBTouchEventsDataset TouchEvents;

        // Emotion Dataset
        public VideoEmotionDataset EmotionDataset;

        // EDA Dataset

        public DatasetParser()
        {
            //ParseTouchEvents();
            //Console.WriteLine(TouchEvents.ToString());
            
            ParseEmotionDataset();
            Console.WriteLine(EmotionDataset.ToString());
            
            //ParseEDADataset();
        }

        private void ParseEDADataset()
        {
            throw new NotImplementedException();
        }

        private void ParseEmotionDataset()
        {
            VideoEmotionDatasetParser parser = new VideoEmotionDatasetParser(EmotionDatasetFilepath);
            EmotionDataset = parser.Dataset;
        }

        private void ParseTouchEvents()
        {
            ADBLogEventsParser parser = new ADBLogEventsParser(TouchEventsFilepath);
            TouchEvents = parser.Dataset;
        }
    }
}
