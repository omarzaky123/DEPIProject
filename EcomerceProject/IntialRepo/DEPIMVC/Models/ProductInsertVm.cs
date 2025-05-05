using DEPIMVC.Repository;
using System.ComponentModel.DataAnnotations;

namespace DEPIMVC.Models
{
    public class ProductInsertVm:IHasImage
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Image { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
        public int CatigoryId { get; set; }
       
        public virtual List<ProductImageInsertVm>? ProductImages { get; set; }=new List<ProductImageInsertVm>();
        [Required]
        public virtual List<IFormFile> ProductImagesFiles { get; set; }

    }
}
