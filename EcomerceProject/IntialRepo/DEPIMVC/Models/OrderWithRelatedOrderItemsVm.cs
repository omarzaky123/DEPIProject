namespace DEPIMVC.Models
{
    public class OrderWithRelatedOrderItemsVm
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }
        public int GusetId { get; set; }

        public IEnumerable<OrderItemVm> OrderItems { get; set; }
    }
}
