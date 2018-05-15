using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using JsonBenchmark.TestDTOs;
using JsonBenchmark.TestDTOs.Json2;
using Newtonsoft.Json;

namespace JsonBenchmark
{

    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonSerializersBenchmarks : JsonBenchmarkBase
    {
        private Root Root => new JsonDeserializersBenchmarks().NewtonsoftJson_Deserialize();
        private Root Root_Stream => new JsonDeserializersBenchmarks().NewtonsoftJson_Deserialize_FromStream();

        [Benchmark]
        public string NewtonsoftJson_Serialize()
        {
            return JsonConvert.SerializeObject(Root);
        }


        [Benchmark]
        public string NewtonsoftJson_Serialize_Stream()
        {
            return JsonConvert.SerializeObject(Root_Stream);
        }
    }
}
