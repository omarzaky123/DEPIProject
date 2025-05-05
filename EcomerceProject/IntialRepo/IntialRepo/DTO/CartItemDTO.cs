using DEPI.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime Added_At =DateTime.Now;
        public int? ProductVartionID { get; set; }
        public int ShoppingCartId { get; set; }
        public ProductVartionDTO? ProductVartion { get; set; }
        

    }
}
