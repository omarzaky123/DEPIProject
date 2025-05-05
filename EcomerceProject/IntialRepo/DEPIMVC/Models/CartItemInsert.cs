namespace DEPIMVC.Models
{
    public class CartItemInsert
    {
        public int Quantity { get; set; }

        public DateTime Added_At = DateTime.Now;
        public int? ProductVartionID { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
