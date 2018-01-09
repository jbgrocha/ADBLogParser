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
        private string EmotionSamplesFilepath = "..\\..\\Resources\\01-Emotions.csv";

        // EDA Dataset FilePath
        private string EDADatasetFilepath = "";

        // Emotion Dataset Parser
        // EDA Dataset Parser

        // Touch Events
        public ADBTouchEventsDataset TouchEvents;

        // Emotion Dataset
        // EDA Dataset

        List<DatasetEntry> Dataset;

        public DatasetParser()
        {
            ParseTouchEvents();
            //Console.WriteLine(TouchEvents.ToString());
            
            //ParseEmotionDataset();
            
            //ParseEDADataset();
        }

        private void ParseEDADataset()
        {
            throw new NotImplementedException();
        }

        private void ParseEmotionDataset()
        {
            throw new NotImplementedException();
        }

        private void ParseTouchEvents()
        {
            ADBLogEventsParser parser = new ADBLogEventsParser(TouchEventsFilepath);
            TouchEvents = parser.Dataset;
        }
    }
}
