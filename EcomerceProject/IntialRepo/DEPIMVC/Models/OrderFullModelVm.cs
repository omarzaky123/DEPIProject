namespace DEPIMVC.Models
{
    public class OrderFullModelVm
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public DateTime Created_At = DateTime.Now;
        public int GusetId { get; set; }
        public GusetFullModel Guset { get; set; }

        public List<OrderItemFullModel> OrderItems { get; set; }
    }
}
