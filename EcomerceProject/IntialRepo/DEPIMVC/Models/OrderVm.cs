using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIMVC.Models
{
    public class OrderVm
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public DateTime Created_At = DateTime.Now;
        public int GusetId { get; set; }

    }
}
