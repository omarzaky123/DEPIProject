using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }

        [ForeignKey("Guset")]
        public int GusetId { get; set; }

        public virtual Guset Guset { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
