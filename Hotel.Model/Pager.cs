using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class Pager<T>
    {

        [JsonConverter(typeof(LongToStringConverter))]
        public long total { set; get; }
        public List<T> rows { set; get; }
    }
}
