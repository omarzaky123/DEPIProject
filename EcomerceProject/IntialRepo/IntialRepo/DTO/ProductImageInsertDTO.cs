using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class ProductImageInsertDTO
    {

        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
