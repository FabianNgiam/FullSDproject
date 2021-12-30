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
        public virtual Game Game { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int StatsId { get; set; }
        public virtual Stats Stats { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
