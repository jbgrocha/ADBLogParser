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

        public DatasetAggregator( int datasetId, ADBTouchEventsDataset touchEvents, VideoEmotionDataset emotionDataset, EDADataset edaDataset)
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
                VideoEmotionDatasetEntry previousEmotion = PreviousEmotion(touchEntry);
                VideoEmotionDatasetEntry nextEmotion = NextEmotion(touchEntry);

                // EDA
                EDADatasetEntry previousEDA = PreviousEDA(touchEntry);
                EDADatasetEntry nextEDA = NextEDA(touchEntry);

                DatasetEntry entry = new DatasetEntry(touchEntry, previousEmotion, nextEmotion, previousEDA, nextEDA);

                Dataset.Entries.Add(entry);
            }
        }

        private VideoEmotionDatasetEntry PreviousEmotion(ADBLogEvent touchEntry)
        {

            VideoEmotionDatasetEntry result = null;

            int index = (int)(touchEntry.Timestamp / VideoEmotionDataset.SamplingRate);
            
            if (index < EmotionDataset.DataEntries.Count)
            {
                result = EmotionDataset.DataEntries[index];
            }

            return result;
        }

        private VideoEmotionDatasetEntry NextEmotion(ADBLogEvent touchEntry)
        {

            VideoEmotionDatasetEntry result = null;

            int index = (int)(touchEntry.Timestamp / VideoEmotionDataset.SamplingRate) + 1;

            if (index < EmotionDataset.DataEntries.Count)
            {
                result = EmotionDataset.DataEntries[index];
            }

            return result;
        }

        private EDADatasetEntry PreviousEDA(ADBLogEvent touchEntry)
        {

            EDADatasetEntry result = null;

            int index = (int)(touchEntry.Timestamp / EDADataset.SamplingRate);

            if (index < EDADataset.DataEntries.Count)
            {
                result = EDADataset.DataEntries[index];
            }

            return result;
        }

        private EDADatasetEntry NextEDA(ADBLogEvent touchEntry)
        {

            EDADatasetEntry result = null;

            int index = (int)(touchEntry.Timestamp / EDADataset.SamplingRate) + 1;

            if (index < EDADataset.DataEntries.Count)
            {
                result = EDADataset.DataEntries[index];
            }

            return result;
        }
    }
}
