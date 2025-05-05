namespace DEPIAPI.DTO
{
    public class OrderItemInsert
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }

        public int? ProductVartionId { get; set; }
    }
}
