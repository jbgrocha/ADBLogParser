using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class RawDatasetAggregator
    {
        // Touch Events
        public SampleDataset TouchDataset;

        // Emotion Dataset
        public VideoEmotionDataset EmotionDataset;

        // EDA Dataset
        public EDADataset EDADataset;

        public RawDataset Dataset;

        public RawDatasetAggregator(string datasetId, SampleDataset touchEvents, VideoEmotionDataset emotionDataset, EDADataset edaDataset)
        {
            TouchDataset = touchEvents;
            EmotionDataset = emotionDataset;
            EDADataset = edaDataset;

            Dataset = new RawDataset(datasetId);

            Agregate();
        }

        private void Agregate()
        {
            foreach (Sample touchEntry in TouchDataset.DataEntries)
            {
                //Emotion
                Tuple<VideoEmotionDatasetEntry, VideoEmotionDatasetEntry> emotionPrevNext = EmotionDataset.GetPreviousNext(touchEntry.Timestamp);

                VideoEmotionDatasetEntry previousEmotion = emotionPrevNext.Item1;
                VideoEmotionDatasetEntry nextEmotion = emotionPrevNext.Item2;


                // EDA
                Tuple<EDADatasetEntry, EDADatasetEntry> edaPrevNext = EDADataset.GetPreviousNext(touchEntry.Timestamp);

                EDADatasetEntry previousEDA = edaPrevNext.Item1;
                EDADatasetEntry nextEDA = edaPrevNext.Item2;

                RawDatasetEntry entry = new RawDatasetEntry(touchEntry, previousEmotion, nextEmotion, previousEDA, nextEDA);

                Dataset.Entries.Add(entry);
            }
        }
    }
}
