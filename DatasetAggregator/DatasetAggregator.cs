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
        public ADBTouchEventsDataset TouchEvents;

        // Emotion Dataset
        public VideoEmotionDataset EmotionDataset;

        // EDA Dataset
        public EDADataset EDADataset;

        public DatasetAggregator( ADBTouchEventsDataset touchEvents, VideoEmotionDataset emotionDataset, EDADataset edaDataset)
        {
            TouchEvents = touchEvents;
            EmotionDataset = emotionDataset;
            EDADataset = edaDataset;
        }

        private void Agregate()
        {
            throw new NotImplementedException();
            /*
             foreach touchEvent in touchEvents
                index of previous = timestamp / sampling rate // round down
                index of next = timestamp / sampling rate  + 1 // round up
                (touchevent, videoprev, videonext, edaprev, edanext)
             */
        }
    }
}
