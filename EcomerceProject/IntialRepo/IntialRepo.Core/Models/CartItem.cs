using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class CartItem
    {

        public int Id { get; set; }

        public int Quantity { get; set; }
        public DateTime Added_At { get; set; }

        [ForeignKey("ProductVartion")]
        public int? ProductVartionID { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }

        public virtual ProductVartion ProductVartion { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
