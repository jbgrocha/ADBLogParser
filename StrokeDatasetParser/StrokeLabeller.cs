using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetParser
{
    public static class StrokeLabeller
    {
        public static void Label(List<Stroke> strokes)
        {
            foreach (Stroke stroke in strokes)
            {
                Neutral(stroke);
                Happy(stroke);
                Sad(stroke);
                Angry(stroke);
                Surprised(stroke);
                Scared(stroke);
                Disgusted(stroke);
                Contempt(stroke);

                Emotion(stroke);

                Valence(stroke);
                Arousal(stroke);

                EDA(stroke);
            }
        }

        private static void Emotion(Stroke stroke)
        {

            KeyValuePair<string,double?>  result = stroke.Emotions.Max();

            stroke.Emotion = result.Key;
        }

        private static void EDA(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEDA.EDA;
            double? next = stroke.RawDatasetEntries.Last().NextEDA.EDA;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEDA.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEDA.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.EDA = result;
        }

        private static void Neutral(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Neutral;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Neutral;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Neutral", result);
        }

        private static void Happy(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Happy;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Happy;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Happy", result);
        }

        private static void Sad(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Sad;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Sad;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Sad", result);
        }

        private static void Angry(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Angry;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Angry;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Angry", result);
        }

        private static void Surprised(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Surprised;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Surprised;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Surprised", result);
        }

        private static void Scared(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Scared;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Scared;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Scared", result);
        }

        private static void Disgusted(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Disgusted;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Disgusted;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Disgusted", result);
        }

        private static void Contempt(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Contempt;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Contempt;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Contempt", result);
        }

        private static void Valence(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Valence;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Valence;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Valence", result);
        }

        private static void Arousal(Stroke stroke)
        {
            double? start = stroke.RawDatasetEntries.Last().PreviousEmotion.Arousal;
            double? next = stroke.RawDatasetEntries.Last().NextEmotion.Arousal;

            double? startTime = stroke.RawDatasetEntries.Last().PreviousEmotion.Timestamp;
            double? nextTime = stroke.RawDatasetEntries.Last().NextEmotion.Timestamp;

            double endTime = stroke.RawDatasetEntries.Last().TouchSample.Timestamp;

            double? result = Interpolate(start, next, startTime, nextTime, endTime);

            stroke.Emotions.Add("Arousal", result);
        }

        private static double? Interpolate(double? start, double? next, double? startTime, double? nextTime, double endTime)
        {
            double? result = null;

            if((start != null) && (next != null))
            {
                result = (next - start) / (nextTime - startTime) * (endTime - startTime);
            }
            else if(start != null)
            {
                result = start;
            }

            return result;
        }
    }
}
