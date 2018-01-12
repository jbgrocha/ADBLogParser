using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class DatasetAggregator
    {
        // Touch Events
        public ADBTouchEventsDataset TouchDataset;

        // Emotion Dataset
        public VideoEmotionDataset EmotionDataset;

        // EDA Dataset
        public EDADataset EDADataset;

        public Dataset Dataset;

        public DatasetAggregator(string datasetId, ADBTouchEventsDataset touchEvents, VideoEmotionDataset emotionDataset, EDADataset edaDataset)
        {
            TouchDataset = touchEvents;
            EmotionDataset = emotionDataset;
            EDADataset = edaDataset;

            Dataset = new Dataset(datasetId);

            Agregate();
        }

        private void Agregate()
        {
            foreach (ADBLogEvent touchEntry in TouchDataset.DataEntries)
            {
                //Emotion
                Tuple<VideoEmotionDatasetEntry, VideoEmotionDatasetEntry> emotionPrevNext = EmotionDataset.GetPreviousNext(touchEntry.Timestamp);

                VideoEmotionDatasetEntry previousEmotion = emotionPrevNext.Item1;
                VideoEmotionDatasetEntry nextEmotion = emotionPrevNext.Item2;


                // EDA
                Tuple<EDADatasetEntry, EDADatasetEntry> edaPrevNext= EDADataset.GetPreviousNext(touchEntry.Timestamp);

                EDADatasetEntry previousEDA = edaPrevNext.Item1;
                EDADatasetEntry nextEDA = edaPrevNext.Item2;

                DatasetEntry entry = new DatasetEntry(touchEntry, previousEmotion, nextEmotion, previousEDA, nextEDA);

                Dataset.Entries.Add(entry);
            }
        }
    }
}
