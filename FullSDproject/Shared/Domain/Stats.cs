using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Stats : BaseDomainModel
    {
        public int CopyID { get; set; }
        public int UserID { get; set; }
        public float Hours { get; set; }
        public int Completion { get; set; }
    }
}