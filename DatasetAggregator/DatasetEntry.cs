namespace DatasetAggregator
{
    public class DatasetEntry
    {
        public Sample TouchSample { get; set; }

        public VideoEmotionDatasetEntry PreviousEmotion { get; set; }
        public VideoEmotionDatasetEntry NextEmotion { get; set; }

        public EDADatasetEntry PreviousEDA { get; set; }
        public EDADatasetEntry NextEDA { get; set; }

        public DatasetEntry(Sample touchSample, VideoEmotionDatasetEntry previousEmotion, VideoEmotionDatasetEntry nextEmotion, EDADatasetEntry previousEDA, EDADatasetEntry nextEDA )
        {
            TouchSample = touchSample;

            PreviousEmotion = previousEmotion;
            NextEmotion = nextEmotion;

            PreviousEDA = previousEDA;
            NextEDA = nextEDA;
        }

        /* need to fix null issue for each of the previous and next elements */

        public override string ToString()
        {
            string result = "";

            result += TouchSample.ToString();

            result += ";";

            if (PreviousEmotion != null)
            {
                result += PreviousEmotion.ToString();
            }
            else
            {
                result += VideoEmotionDatasetEntry.Null;
            }

            result += ";";

            if(NextEmotion != null)
            {
                result += NextEmotion.ToString();
            }
            else
            {
                result += VideoEmotionDatasetEntry.Null;
            }

            result += ";";

            if (PreviousEDA != null)
            {
                result += PreviousEDA.ToString();
            }
            else
            {
                result += EDADatasetEntry.Null;
            }

            result += ";";

            if (NextEDA != null)
            {
                result += NextEDA.ToString();
            }
            else
            {
                result += EDADatasetEntry.Null;
            }

            return result;
        }
    }
}