using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using JsonBenchmark.TestDTOs;
using JsonBenchmark.TestDTOs.Chuck;
using JsonBenchmark.TestDTOs.Json2;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonSerializersBenchmarks : JsonBenchmarkBase
    {
        //private RootChuck RootChuck => new JsonDeserializersBenchmarks().NewtonsoftJson_Deserialize();
        //private RootChuck RootChuckStream => new JsonDeserializersBenchmarks().NewtonsoftJson_Deserialize_FromStream();
        //private RootJson2 RootJson2 => new JsonDeserializersBenchmarks().NewtonsoftJson_Deserialize_Json2();

        //[Benchmark]
        //public string NewtonsoftJson_Serialize()
        //{
        //    return JsonConvert.SerializeObject(RootChuck);
        //}

        //[Benchmark]
        //public string NewtonsoftJson_Serialize_Stream()
        //{
        //    return JsonConvert.SerializeObject(RootChuckStream);
        //}

        //[Benchmark]
        //public string JavaScriptSerializer_Serialize()
        //{
        //    return JavaScriptSerializer.Serialize(RootChuck);
        //}

        //[Benchmark]
        //public string DataContractSerializer_Serialize()
        //{
        //    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(RootChuck));
        //    using (var msObj = new MemoryStream())
        //    using (var sr = new StreamReader(msObj))
        //    {
        //        js.WriteObject(msObj, RootChuck);
        //        msObj.Position = 0;
        //        return sr.ReadToEnd();
        //    }
        //}

        //[Benchmark]
        //public string NewtonsoftJson_Serialize_Json2()
        //{
        //    return JsonConvert.SerializeObject(RootJson2);
        //}

        //[Benchmark]
        //public string JavaScriptSerializer_Serialize_Json2()
        //{
        //    return JavaScriptSerializer.Serialize(RootJson2);
        //}
    }
}
