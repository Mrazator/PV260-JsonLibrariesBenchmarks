using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;
using JsonBenchmark.TestDTOs;
using JsonBenchmark.TestDTOs.Chuck;
using JsonBenchmark.TestDTOs.Json2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonDeserializersBenchmarks : JsonBenchmarkBase
    {
        #region NewtonsoftJson Deserialize From String

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize()
        {
            return JsonConvert.DeserializeObject<RootChuck>(Chuck);
        }

        public RootChuck NewtonsoftJson_Deserialize_ForSerialization()
        {
            return JsonConvert.DeserializeObject<RootChuck>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json")));
        }

        [Benchmark]
        public RootJson2 NewtonsoftJson_Deserialize_Json2()
        {
            return JsonConvert.DeserializeObject<RootJson2>(Json2);
        }

        public RootJson2 NewtonsoftJson_Deserialize_Json2_ForSerialization()
        {
            return JsonConvert.DeserializeObject<RootJson2>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "json2.json")));
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_Dynamic()
        {
            return JsonConvert.DeserializeObject<dynamic>(Chuck).ToObject<RootChuck>();
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_Parse()
        {
            dynamic receivedObject = JObject.Parse(Chuck);
            return receivedObject.ToObject<RootChuck>();
        }

        [Benchmark]
        public JObject NewtonsoftJson_Deserialize_Parse_JObject()
        {
            dynamic receivedObject = JObject.Parse(Chuck);
            return receivedObject;
        }

        #endregion

        #region NewtonsoftJson Deserialize From Stream

        private StreamReader _file;

        [IterationSetup(Target = nameof(NewtonsoftJson_Deserialize_FromStream))]
        public void NewtonSoftJson_Deserialize_FromStream_SetUp()
        {
            _file =
                File.OpenText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_FromStream()
        {
            return (RootChuck)JsonSerializer.Deserialize(_file, typeof(RootChuck));
        }

        [IterationCleanup(Target = nameof(NewtonsoftJson_Deserialize_FromStream))]
        public void NewtonSoftJson_Deserialize_FromStream_CleanUp()
        {
            _file.Dispose();
        }

        private StreamReader _file2;

        [IterationSetup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_Json2))]
        public void NewtonSoftJson_Deserialize_FromStream_Json2_SetUp()
        {
            _file2 =
                File.OpenText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "json2.json"));
        }

        [Benchmark]
        public RootJson2 NewtonsoftJson_Deserialize_FromStream_Json2()
        {
            return (RootJson2) JsonSerializer.Deserialize(_file2, typeof(RootJson2));
        }

        [IterationCleanup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_Json2))]
        public void NewtonSoftJson_Deserialize_FromStream_Json2_CleanUp()
        {
            _file2.Dispose();
        }

        #endregion

        #region NewtonsoftJson Deserialize From Stream While Reader.Read()

        private JsonReader _reader;
        private StreamReader _fileReader;

        [IterationSetup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_Reader))]
        public void NewtonsoftJson_Deserialize_FromStream_Reader_SetUp()
        {
            _fileReader =
                File.OpenText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
            _reader = new JsonTextReader(_fileReader);
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_FromStream_Reader()
        {
            while (_reader.Read())
            {
                return JsonSerializer.Deserialize<RootChuck>(_reader);
            }

            return null;
        }

        [IterationCleanup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_Reader))]
        public void NewtonsoftJson_Deserialize_FromStream_Reader_CleanUp()
        {
            _fileReader.Dispose();
        }

        #endregion

        #region NewtonsoftJson Deserialize From Stream While !EndOfStream

        private StreamReader _fileEndOfStream;
        private JsonReader _readerEndOfStream;

        [IterationSetup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_EndOfStream))]
        public void NewtonsoftJson_Deserialize_FromStream_EndOfStream_SetUp()
        {
            _fileEndOfStream =
                File.OpenText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json"));
            _readerEndOfStream = new JsonTextReader(_fileEndOfStream);
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_FromStream_EndOfStream()
        {
            while (!_fileEndOfStream.EndOfStream)
            {
                return JsonSerializer.Deserialize<RootChuck>(_readerEndOfStream);
            }

            return null;
        }

        [IterationCleanup(Target = nameof(NewtonsoftJson_Deserialize_FromStream_EndOfStream))]
        public void NewtonsoftJson_Deserialize_FromStream_EndOfStream_CleanUp()
        {
            _fileEndOfStream.Dispose();
        }

        #endregion

        #region DataContract Deserialize From Stream

        //not perfectly optimized, optimized example commented below doesnt work 
        [Benchmark]
        public RootChuck DataContract_Deserialize()
        {
            var dc = new DataContractJsonSerializer(typeof(RootChuck));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(Chuck)))
            {
                return (RootChuck) dc.ReadObject(ms);
            }
        }

        //This doesnt work, i am not sure why

        //private MemoryStream _memoryStream;

        //[IterationSetup(Target = nameof(DataContractSerializer_Deserialize))]
        //public void DataContractSerializer_SetUp()
        //{
        //    _memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(Chuck));
        //}

        //[Benchmark]
        //public RootChuck DataContractSerializer_Deserialize()
        //{
        //    return (RootChuck) DataContractJsonSerializer.ReadObject(_memoryStream);
        //}

        //[IterationCleanup(Target = nameof(DataContractSerializer_Deserialize))]
        //public void DataContractSerializer_CleanUp()
        //{
        //    _memoryStream.Dispose();
        //}

        #endregion

        #region JavaScriptSerializer Deserialize From String

        [Benchmark]
        public RootChuck JavaScriptSerializer_Deserialize()
        {
            return JavaScriptSerializer.Deserialize<RootChuck>(Chuck);
        }

        //Throws exception: The length of the string exceeds the value set on the maxJsonLength property
        //[Benchmark]
        //public RootJson2 JavaScriptSerializer_Deserialize_Json2()
        //{
        //    return JavaScriptSerializer.Deserialize<RootJson2>(Json2);
        //}

        #endregion

        #region HaveBoxJSON Deserialize From String

        //not supported in this .net version
        //[Benchmark]
        //public RootChuck HaveBoxJson_Deserialize()
        //{
        //    return JsonConverter.Deserialize<RootChuck>(Chuck);
        //}

        //[Benchmark]
        //public RootJson2 HaveBoxJson_Deserialize_Json2()
        //{
        //    return JsonConverter.Deserialize<RootJson2>(Json2);
        //}

        #endregion

        #region Utf8JSON Deserialize From String

        [Benchmark]
        public RootChuck Utf8Json_Deserialize()
        {
            return Utf8Json.JsonSerializer.Deserialize<RootChuck>(Chuck);
        }

        [Benchmark]
        public RootJson2 Utf8Json_Deserialize_Json2()
        {
            return Utf8Json.JsonSerializer.Deserialize<RootJson2>(Json2);
        }

        #endregion
    }
}
