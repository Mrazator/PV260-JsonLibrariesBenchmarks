using System.IO;
using System.Linq;
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
        [GlobalCleanup]
        public void DisposeObjects()
        {
            StreamReader.Dispose();
            JsonSampleStream.Dispose();
        }

        //Looks like it is much much faster than from string
        //up to 170 000 faster!!! or i am doing something wrong :)
        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_FromStream()
        {
            while (Reader.Read())
            {
                return new JsonSerializer().Deserialize<RootChuck>(Reader);
            }

            return null;
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize()
        {
            return JsonConvert.DeserializeObject<RootChuck>(JsonSampleString);
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_Dynamic()
        {
            return JsonConvert.DeserializeObject<dynamic>(JsonSampleString).ToObject<RootChuck>();
        }

        [Benchmark]
        public RootChuck NewtonsoftJson_Deserialize_Parse()
        {
            dynamic receivedObject = JObject.Parse(JsonSampleString);
            return receivedObject.ToObject<RootChuck>();
        }

        [Benchmark]
        public JObject NewtonsoftJson_Deserialize_Parse_JObject()
        {
            dynamic receivedObject = JObject.Parse(JsonSampleString);
            return receivedObject;
        }

        [Benchmark]
        public RootChuck JavaScriptSerializer_Deserialize()
        {
            return JavaScriptSerializer.Deserialize<RootChuck>(JsonSampleString);
        }

        [Benchmark]
        public RootJson2 NewtonsoftJson_Deserialize_Json2()
        {
            return JsonConvert.DeserializeObject<RootJson2>(JsonSampleString2);
        }

        [Benchmark]
        public RootJson2 JavaScriptSerializer_Deserialize_Json2()
        {
            return JavaScriptSerializer.Deserialize<RootJson2>(JsonSampleString2);
        }
    }
}
