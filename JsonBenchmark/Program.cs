using System;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace JsonBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<JsonDeserializersBenchmarks>();
            //BenchmarkRunner.Run<JsonSerializersBenchmarks>();

            // var bench = new JsonDeserializersBenchmarks();
            // bench.NewtonSoftJson_Deserialize_FromStream_SetUp();
            //var a= bench.NewtonsoftJson_Deserialize_FromStream();
            // bench.NewtonSoftJson_Deserialize_FromStream_CleanUp();

            // bench.NewtonsoftJson_Deserialize_FromStream_EndOfStream_SetUp();
            // var b = bench.NewtonsoftJson_Deserialize_FromStream_EndOfStream();
            // bench.NewtonsoftJson_Deserialize_FromStream_EndOfStream_CleanUp();

            // bench.NewtonsoftJson_Deserialize_FromStream_Reader_SetUp();
            // var c = bench.NewtonsoftJson_Deserialize_FromStream_Reader();
            // bench.NewtonsoftJson_Deserialize_FromStream_Reader_CleanUp();
        }
    }
}
