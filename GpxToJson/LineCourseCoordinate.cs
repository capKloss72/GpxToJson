using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackerconfig.Props
{
    class LineCourseCoordinate
    {
        public LineCourseCoordinate()
        {

        }

        public LineCourseCoordinate(double Lat, double Lon, int Ele = 0)
        {
            lat = Lat;
            lon = Lon;
            ele = ele;
        }

        public double lat { get; set; }
        public double lon { get; set; }
        public int ele { get; set; }
    }
}
