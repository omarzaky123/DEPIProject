using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class ProductInsertDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        public int CatigoryId { get; set; }
        public virtual List<ProductImageInsertDTO> ProductImages { get; set; }
    }
}
