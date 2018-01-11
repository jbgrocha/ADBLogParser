namespace DatasetAggregator
{
    public class DatasetEntry
    {
        public ADBLogEvent TouchEvent { get; set; }

        public VideoEmotionDatasetEntry PreviousEmotion { get; set; }
        public VideoEmotionDatasetEntry NextEmotion { get; set; }

        public EDADatasetEntry PreviousEDA { get; set; }
        public EDADatasetEntry NextEDA { get; set; }

        public DatasetEntry(ADBLogEvent touch, VideoEmotionDatasetEntry previousEmotion, VideoEmotionDatasetEntry nextEmotion, EDADatasetEntry previousEDA, EDADatasetEntry nextEDA )
        {
            TouchEvent = touch;

            PreviousEmotion = previousEmotion;
            NextEmotion = nextEmotion;

            PreviousEDA = previousEDA;
            NextEDA = nextEDA;
        }

        /* need to fix null issue for each of the previous and next elements */

        public override string ToString()
        {
            string result = "";

            result += TouchEvent.ToString();

            if(PreviousEmotion != null)
            {
                result += PreviousEmotion.ToString();
            }

            if(NextEmotion != null)
            {
                result += NextEmotion.ToString();
            }

            if(PreviousEDA != null)
            {
                result += PreviousEDA.ToString();
            }
            
            if(NextEDA != null)
            {
                result += NextEDA.ToString();
            }
            
            return result;
        }
    }
}