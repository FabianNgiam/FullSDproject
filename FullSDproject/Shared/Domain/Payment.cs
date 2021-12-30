using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        public int CreditCard { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public String FullName { get; set; }
        public String Address { get; set; }
    }
}
