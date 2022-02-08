using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public String CreditCard { get; set; }
        public String FullName { get; set; }
        public String Address { get; set; }
        public int expiryMonth { get; set; }
        public int expiryYear { get; set; }
        public int CVV { get; set; }
    }
}
