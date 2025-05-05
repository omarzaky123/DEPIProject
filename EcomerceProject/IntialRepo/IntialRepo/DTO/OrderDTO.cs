using DEPI.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }
        public int GusetId { get; set; }
        public virtual GusetFullModelDTO Guset { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
