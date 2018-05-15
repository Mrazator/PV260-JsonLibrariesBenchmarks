using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;
using JsonBenchmark.TestDTOs;
using JsonBenchmark.TestDTOs.Json2;
using Newtonsoft.Json;

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
        public Root NewtonsoftJson_Deserialize_FromStream()
        {
            while (Reader.Read())
            {
                return new JsonSerializer().Deserialize<Root>(Reader);
            }

            return null;
        }

        [Benchmark]
        public Root NewtonsoftJson_Deserialize()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleString);
        }

        [Benchmark]
        public Root2 NewtonsoftJson_Deserialize_Json2()
        {
            return JsonConvert.DeserializeObject<Root2>(JsonSampleString2);
        }
    }
}
