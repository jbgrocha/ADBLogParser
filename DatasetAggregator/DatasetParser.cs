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
        public ADBTouchEventsDataset TouchEvents;

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
            EDADatasetParser parser = new EDADatasetParser(EDADatasetFilepath);
            EDADataset = parser.Dataset;
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
