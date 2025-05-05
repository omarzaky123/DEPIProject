namespace DEPIMVC.Models
{
    public class OrderItemFullModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public int? ProductVartionId { get; set; }

        public ProductVationVm ProductVartion { get; set; }
    }
}
