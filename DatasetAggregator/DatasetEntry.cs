namespace DatasetAggregator
{
    public class DatasetEntry
    {
        public ADBLogEvent Touch { get; set; }

        public VideoEmotionDatasetEntry PreviousEmotion { get; set; }
        public VideoEmotionDatasetEntry NextEmotion { get; set; }

        public EDADatasetEntry PreviousEDA { get; set; }
        public EDADatasetEntry NextEDA { get; set; }

        public DatasetEntry(ADBLogEvent touch, VideoEmotionDatasetEntry previousEmotion, VideoEmotionDatasetEntry nextEmotion, EDADatasetEntry previousEDA, EDADatasetEntry nextEDA )
        {
            Touch = touch;

            PreviousEmotion = previousEmotion;
            NextEmotion = nextEmotion;

            PreviousEDA = previousEDA;
            NextEDA = nextEDA;
        }
    }
}