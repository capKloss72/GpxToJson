using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackerconfig.Json
{
    class Properties
    {

        public Properties(bool Courseline = false, bool StartLine = false, bool FinishLine = false, bool Boundingboxpolygon = false)
        {
            courseline = Courseline;
            startline = StartLine;
            finishline = FinishLine;
            boundingboxpolygon = Boundingboxpolygon;
        }

        [JsonProperty(PropertyName = "course-line")]
        [DefaultValue(false)]
        public bool courseline { get; set; }

        [JsonProperty(PropertyName = "start-line")]
        [DefaultValue(false)]
        public bool startline { get; set; }

        [JsonProperty(PropertyName = "finish-line")]
        [DefaultValue(false)]
        public bool finishline { get; set; }

        [JsonProperty(PropertyName = "boundingbox-polygon")]
        [DefaultValue(false)]
        public bool boundingboxpolygon { get; set; }
    }
}
