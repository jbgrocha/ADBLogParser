﻿using RawDatasetGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeDatasetGenerator
{
    public class Stroke
    {
        public List<RawDatasetEntry> RawDatasetEntries { get; set; }
        public Dictionary<string, double?> Features { get; set; }
        public Dictionary<string, double?> Emotions { get; set; }

        public double? EDA { get; set; }

        public string Emotion { get; set; }

        public Stroke()
        {
            RawDatasetEntries = new List<RawDatasetEntry>();
            Features = new Dictionary<string, double?>();
            Emotions = new Dictionary<string, double?>();
        }

        public Stroke(List<RawDatasetEntry> rawDatasetEntries)
        {
            RawDatasetEntries = rawDatasetEntries;
            Features = new Dictionary<string, double?>();
            Emotions = new Dictionary<string, double?>();
        }

        public override string ToString()
        {
            string result = "";

            result += FeaturesToString();

            result += " Emotions: " + EmotionsToString() + " EDA: " + EDA.ToString() + " Emotion: " + Emotion + ";";

            return result;
        }

        private string EmotionsToString()
        {
            string result = "";

            foreach(KeyValuePair<string, double?> emotion in Emotions)
            {
                result += emotion.Key + " " + emotion.Value + "\t";
            }

            result += ";";

            return result;
        }

        public string FeaturesToString()
        {
            string result = "Features: ";

            foreach (KeyValuePair<string,double?> kvp in Features)
            {
                result += kvp.Key + ": " + kvp.Value + " ";
            }

            result += ";";

            return result;
        }
    }
}
