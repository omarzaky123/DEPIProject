using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("ProductVartion")]
        public int? ProductVartionId { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductVartion ProductVartion { get; set; }
    }
}
