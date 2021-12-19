using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Game : BaseDomainModel
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string Requirements { get; set; }
        public string Rating { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
