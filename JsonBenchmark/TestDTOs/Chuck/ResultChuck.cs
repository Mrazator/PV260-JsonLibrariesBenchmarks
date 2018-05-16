using System.Runtime.Serialization;

namespace JsonBenchmark.TestDTOs.Chuck
{
    [DataContract]
    public class ResultChuck
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string IconUrl { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string[] Category { get; set; }
    }
}