using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackervonfig.Props
{
    class RaceDataCSV : IEventData
    {
        public string RaceId { get; set; }
        public string RaceName { get; set; }
        public int IsCurrent { get; set; }
    }
}
