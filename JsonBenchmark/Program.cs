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
        }
    }
}
