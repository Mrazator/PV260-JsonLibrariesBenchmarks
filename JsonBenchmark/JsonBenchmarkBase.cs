using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using BenchmarkDotNet.Attributes;
using JsonBenchmark.TestDTOs.Chuck;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    public abstract class JsonBenchmarkBase
    {
        protected const string TestFilesFolder = "TestFiles";

        protected string Chuck;
        protected string Json2;

        protected JsonSerializer JsonSerializer;
        protected JavaScriptSerializer JavaScriptSerializer;
        protected DataContractJsonSerializer DataContractJsonSerializer;

        [GlobalSetup]
        public void SetUp()
        {
            Chuck = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
            Json2 = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "json2.json"));

            JsonSerializer = new JsonSerializer();
            JavaScriptSerializer = new JavaScriptSerializer();
            DataContractJsonSerializer = new DataContractJsonSerializer(typeof(RootChuck));
        }
    }
}
