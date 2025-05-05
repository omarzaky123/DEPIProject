using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public DateTime Created_At { get; set; }

        [ForeignKey("Guset")]
        public int GusetId { get; set; }
        public virtual Guset Guset { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
