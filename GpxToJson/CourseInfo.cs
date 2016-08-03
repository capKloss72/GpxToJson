using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxToJson
{
    class CourseInfo
    {
        public string type { get; set; }
        public Feature[] features { get; set; }
    }
}
