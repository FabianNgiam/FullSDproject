using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Copy : BaseDomainModel
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int StatsId { get; set; }
        public int OrderId { get; set; }
    }
}
