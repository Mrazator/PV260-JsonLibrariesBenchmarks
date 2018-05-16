using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JsonBenchmark.TestDTOs.Chuck
{
    [DataContract]
    public class RootChuck
    {
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public ResultChuck[] Result { get; set; }
    }
}
