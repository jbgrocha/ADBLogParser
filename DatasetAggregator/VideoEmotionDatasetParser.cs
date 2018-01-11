using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAggregator
{
    public class VideoEmotionDatasetParser
    {
        public VideoEmotionDataset Dataset;

        private string FilePath;
        private List<string> FileLines;

        public VideoEmotionDatasetParser(string filePath)
        {
            Dataset = new VideoEmotionDataset();

            FilePath = filePath;
            ReadFile();
            ParseDataset();
            NormalizeDatasetTime();
        }

        private void NormalizeDatasetTime()
        {
            double timeStamp = 0.0;
            double samplingRate = VideoEmotionDataset.SamplingRate;

            for (int i = 0; i < Dataset.DataEntries.Count; i++)
            {
                if(i != 0)
                {
                    timeStamp = timeStamp + samplingRate;
                }

                Dataset.DataEntries[i].Timestamp = timeStamp;
            }
        }

        private void ParseDataset()
        {
            for(int i = 0; i < FileLines.Count; i++)
            {
                string line = FileLines.ElementAt(i);

                if (i != 0)
                {
                    ParseValues(line);
                }
            }
        }

        private void ParseValues(string line)
        {
            string[] splitLine = line.Split(';');

            //List<double> datasetEntry = new List<double>();

            VideoEmotionDatasetEntry datasetEntry = new VideoEmotionDatasetEntry
            {
                Timestamp = ParseTime(splitLine.ElementAt(0)),

                Neutral = double.Parse(splitLine.ElementAt(1)),
                Happy = double.Parse(splitLine.ElementAt(2)),
                Sad = double.Parse(splitLine.ElementAt(3)),
                Angry = double.Parse(splitLine.ElementAt(4)),
                Surprised = double.Parse(splitLine.ElementAt(5)),
                Scared = double.Parse(splitLine.ElementAt(6)),
                Disgusted = double.Parse(splitLine.ElementAt(7)),
                Contempt = double.Parse(splitLine.ElementAt(8)),
                Valence = double.Parse(splitLine.ElementAt(9)),
                Arousal = double.Parse(splitLine.ElementAt(10))
            };

            Dataset.DataEntries.Add(datasetEntry);
        }

        // Parses time to Milliseconds
        private double ParseTime(string timeStamp)
        {
            double result = 0.0;

            string[] split = timeStamp.Split(':');

            // Minutes to Milliseconds
            result = double.Parse(split[0]) * 60 * 1000;

            // Seconds to Milliseconds
            result += double.Parse(split[1]) * 1000;

            return result;
        }

        private void ReadFile()
        {
            try
            {
                FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }
    }
}
