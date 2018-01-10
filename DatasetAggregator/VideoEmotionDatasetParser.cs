﻿using System;
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

                if ( i == 0)
                {
                    ParseLabels(line);
                }
                else if(i != 0)
                {
                    ParseValues(line);
                }
            }
        }

        private void ParseLabels(string line)
        {
            string[] splitLine = line.Split(';');

            Dataset.Labels = splitLine.ToList<string>();
        }

        private void ParseValues(string line)
        {
            string[] splitLine = line.Split(';');

            //List<double> datasetEntry = new List<double>();

            VideoEmotionDatasetEntry datasetEntry = new VideoEmotionDatasetEntry();

            for(int i = 0; i < splitLine.Length; i++)
            {
                double parsedValue = 0.0;

                if (i == 0)
                {
                    datasetEntry.Timestamp = ParseTime(splitLine.ElementAt(i));
                }
                else
                {
                    string key = Dataset.Labels[i];

                    parsedValue = double.Parse(splitLine.ElementAt(i));

                    datasetEntry.Labels.Add(key, parsedValue);
                }
            }

            //List<double> datasetEntry = splitLine.Select(double.Parse).ToList<double>();

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