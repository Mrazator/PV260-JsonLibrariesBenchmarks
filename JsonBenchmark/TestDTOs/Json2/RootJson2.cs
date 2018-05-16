using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonBenchmark.TestDTOs.Json2
{
    [DataContract]
    public class RootJson2
    {
        [DataMember]
        public string squadName { get; set; }
        [DataMember]
        public ResultJson2[] members { get; set; }
    }
}
