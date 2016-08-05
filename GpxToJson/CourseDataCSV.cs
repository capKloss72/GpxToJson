using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackerconfig.Props
{
    class CourseDataCSV
    {
        public string GPXLocation { get; set; }
        public string RaceId { get; set; }
        public string OutputJsonLocation { get; set; }
        public double StartLatRight { get; set; }
        public double StartLonRight { get; set; }
        public double StartLatLeft { get; set; }
        public double StartLonLeft { get; set; }
        public double FinishLatRight { get; set; }
        public double FinishLonRight { get; set; }
        public double FinishLatLeft { get; set; }
        public double FinishLonLeft { get; set; }
    }
}
