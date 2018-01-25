using EDAParser;
using SampleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoParser;

namespace StrokeDatasetGenerator
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

                //Emotion(stroke);

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
            EDADatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEDA;
            EDADatasetEntry next = stroke.RawDatasetEntries.Last().NextEDA;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.EDA, next.EDA, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.EDA = result;
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.EDA);

                stroke.EDA = result;
            }
        }

        private static void Neutral(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Neutral, next.Neutral, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Neutral", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Neutral);

                stroke.Emotions.Add("Neutral", result);
            }
        }

        private static void Happy(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Happy, next.Happy, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Happy", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Happy);

                stroke.Emotions.Add("Happy", result);
            }
        }

        private static void Sad(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Sad, next.Sad, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Sad", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Sad);

                stroke.Emotions.Add("Sad", result);
            }
        }

        private static void Angry(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Angry, next.Angry, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Angry", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Angry);

                stroke.Emotions.Add("Angry", result);
            }

        }

        private static void Surprised(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Surprised, next.Surprised, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Surprised", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Surprised);

                stroke.Emotions.Add("Surprised", result);
            }
        }

        private static void Scared(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Scared, next.Scared, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Scared", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Scared);

                stroke.Emotions.Add("Scared", result);
            }

        }

        private static void Disgusted(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Disgusted, next.Disgusted, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Disgusted", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Disgusted);

                stroke.Emotions.Add("Disgusted", result);
            }
        }

        private static void Contempt(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Contempt, next.Contempt, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Contempt", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Contempt);

                stroke.Emotions.Add("Contempt", result);
            }
        }

        private static void Valence(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Valence, next.Valence, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Valence", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Valence);

                stroke.Emotions.Add("Valence", result);
            }
        }

        private static void Arousal(Stroke stroke)
        {
            VideoEmotionDatasetEntry previous = stroke.RawDatasetEntries.Last().PreviousEmotion;
            VideoEmotionDatasetEntry next = stroke.RawDatasetEntries.Last().NextEmotion;

            Sample touch = stroke.RawDatasetEntries.Last().TouchSample;

            if ((previous != null) && (next != null))
            {
                double? result = Interpolate(previous.Arousal, next.Arousal, previous.Timestamp, next.Timestamp, touch.Timestamp);

                stroke.Emotions.Add("Arousal", result);
            }
            else if (previous != null)
            {
                double? result = Interpolate(previous.Arousal);

                stroke.Emotions.Add("Arousal", result);
            }
        }

        private static double? Interpolate(double? start, double? next, double? startTime, double? nextTime, double endTime)
        {
            double? result = null;

            result = start +  (next - start) / (nextTime - startTime) * (endTime - startTime);

            return result;
        }

        private static double? Interpolate(double? start)
        {
            double? result = null;
            result = start;
            return result;
        }
    }
}
