using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class ProductVartion
    {

        public int Id { get; set; }
        public string VartionValue { get; set; }
        public decimal AddtionalPrice { get; set; }
        public int Quantity_In_Stock { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [ForeignKey("Vartion")]
        public int? VartionID { get; set; }

        public virtual Vartion Vartion { get; set; }    
        public virtual Product Product { get; set; }
        
        public virtual ICollection<CartItem> CartItems{ get;set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
