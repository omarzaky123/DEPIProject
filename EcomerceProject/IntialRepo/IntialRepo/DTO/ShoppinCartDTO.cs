using DEPI.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class ShoppinCartDTO
    {
        public int Id { get; set; }

        public DateTime Created_At = DateTime.Now;
        public int GusetId { get; set; }
    }
}
