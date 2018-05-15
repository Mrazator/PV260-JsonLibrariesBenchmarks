using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBenchmark.TestDTOs.Json2
{
    public class RootJson2
    {
        public string squadName { get; set; }
        public ResultJson2[] members { get; set; }
    }
}
