namespace DEPIMVC.Models
{
    public class ShoppingCartVm
    {
        public int Id { get; set; }

        public DateTime Created_At = DateTime.Now;
        public int GusetId { get; set; }
    }
}
