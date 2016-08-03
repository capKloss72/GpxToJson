using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxToJson
{
    class Geometry
    {
        public string type { get; set; }
        public object[,] coordinates { get; set; }
    }
}
