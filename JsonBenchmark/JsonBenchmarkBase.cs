using System;
using System.IO;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    public abstract class JsonBenchmarkBase
    {
        private const string TestFilesFolder = "TestFiles";
        protected string JsonSampleString;
        protected string JsonSampleString2;
        protected Stream JsonSampleStream;
        protected StreamReader StreamReader;
        protected JsonReader Reader;

        protected JsonBenchmarkBase()
        {
            JsonSampleString = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
            JsonSampleString2 = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "json2.json"));
            JsonSampleStream = File.OpenRead(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
            StreamReader = new StreamReader(JsonSampleStream);
            Reader = new JsonTextReader(StreamReader);
        }
    }
}
