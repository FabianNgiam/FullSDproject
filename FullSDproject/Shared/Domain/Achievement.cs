using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Achievement : BaseDomainModel
    {
        public int StatsID { get; set; }
        public String Condition { get; set; }
        public bool Fulfillment { get; set; }
    }
}
