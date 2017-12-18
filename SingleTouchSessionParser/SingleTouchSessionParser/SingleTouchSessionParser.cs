
using Sessions;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System;

namespace SingleTouchSessionParser
{
    public class SingleTouchSessionParser
    {
        private string FilePath;
        private string SessionJSON;
        public Session Session;

        public SingleTouchSessionParser(string filepath)
        {
            FilePath = filepath;
            ReadFile();
            Parse();
        }

        private void ReadFile()
        {
            try
            {
                SessionJSON = File.ReadAllText(FilePath);
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The read operation could not be performed because the specified part of the file is locked.", e.GetType().Name);
            }
        }

        private void Parse()
        {
            Session = JsonConvert.DeserializeObject<Session>(SessionJSON);
        }
    }
}
