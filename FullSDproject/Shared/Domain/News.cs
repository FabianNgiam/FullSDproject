using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class News : BaseDomainModel
    {
        public int GameID { get; set; }
        public virtual Game Game { get; set; }
        public string Writer { get; set; }
        public string NewsEntry { get; set; }
    }
}
