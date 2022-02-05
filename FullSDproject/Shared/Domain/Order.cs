using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDproject.Shared.Domain
{
    public class Order : BaseDomainModel
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
    }
}
