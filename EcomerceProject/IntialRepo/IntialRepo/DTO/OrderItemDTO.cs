using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        
        public int? ProductVartionId { get; set; }

        public ProductVartionDTO ProductVartion { get; set; }
    }
}
