using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoParser
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

            if(File.Exists(FilePath))
            {
                ReadFile();
                ParseDataset();
            }
        }

        private void ParseDataset()
        {
            for(int i = 0; i < FileLines.Count; i++)
            {
                string line = FileLines.ElementAt(i);

                if (i != 0)
                {
                    ParseValues(line, i);
                }
            }
        }

        private void ParseValues(string line, int lineNumber)
        {
            string[] splitLine = line.Split(';');

            // "correct implementation" needs to validate all entries in splitLines, but we can just assume this simplifcation
            if ((splitLine[1] != "FIND_FAILED") && (splitLine[1] != "FIT_FAILED"))
            {
                VideoEmotionDatasetEntry datasetEntry = new VideoEmotionDatasetEntry
                {
                    Timestamp = (lineNumber - 1) * VideoEmotionDataset.SamplingRate,

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
        }

        private void ReadFile()
        {
            try
            {
                FileLines = new List<string>(File.ReadAllLines(FilePath));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
