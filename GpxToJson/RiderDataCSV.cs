using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackervonfig.Props
{
    class RiderDataCSV : ICSVEventData
    {
        public string RiderId { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public int IsCurrent { get; set; }
    }
}
