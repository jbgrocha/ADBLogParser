using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoParser
{
    public class VideoEmotionDatasetEntry
    {
        public double Timestamp { get; set;}

        public double Neutral { get; set; }
        public double Happy { get; set; }
        public double Sad { get; set; }
        public double Angry { get; set; }
        public double Surprised { get; set; }
        public double Scared { get; set; }
        public double Disgusted { get; set; }
        public double Contempt { get; set; }
        public double Valence { get; set; }
        public double Arousal { get; set; }

        public const string Headers = "Timestamp;Neutral;Happy;Sad;Angry;Surprised;Scared;Disgusted;Contempt;Valence;Arousal";
        public const string PreviousHeaders = "Previous Emotion Timestamp;Neutral;Happy;Sad;Angry;Surprised;Scared;Disgusted;Contempt;Valence;Arousal";
        public const string NextHeaders = "Next Emotion Timestamp;Neutral;Happy;Sad;Angry;Surprised;Scared;Disgusted;Contempt;Valence;Arousal";
        public const string Null = ";;;;;;;;;;";

        public VideoEmotionDatasetEntry()
        {
            Timestamp = 0.0;

            Neutral = 0.0;
            Happy = 0.0;
            Sad = 0.0;
            Angry = 0.0;
            Surprised = 0.0;
            Scared = 0.0;
            Disgusted = 0.0;
            Contempt = 0.0;
            Valence = 0.0;
            Arousal = 0.0;
        }

        public VideoEmotionDatasetEntry(double timestamp, 
            double neutral, double happy, double sad, double angry, double surprised, double scared, double disgusted, double contempt, double valence, double arousal)
        {
            Timestamp = timestamp;

            Neutral = neutral;
            Happy = happy;
            Sad = sad;
            Angry = angry;
            Surprised = surprised;
            Scared = scared;
            Disgusted = disgusted;
            Contempt = contempt;
            Valence = valence;
            Arousal = arousal;
        }

        public override string ToString()
        {
            string result = Timestamp.ToString();

            result += ";" + Neutral.ToString();
            result += ";" + Happy.ToString();
            result += ";" + Sad.ToString();
            result += ";" + Angry.ToString();
            result += ";" + Surprised.ToString();
            result += ";" + Scared.ToString();
            result += ";" + Disgusted.ToString();
            result += ";" + Contempt.ToString();
            result += ";" + Valence.ToString();
            result += ";" + Arousal.ToString();

            return result;
        }
    }
}
