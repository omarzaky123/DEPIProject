using DEPI.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace DEPIAPI.DTO
{
    public class ProductDTO
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
        [Required]
        public string CatigoryName { get; set; }
        public string CatigoryDescription { get; set; }
        public string VartionName { get; set; }
        public int TotalNumberOfStock { get; set; }
        public IEnumerable<ProductDTO> RelatedProduct { get; set; }
        public IEnumerable<ProductImageInsertDTO> ProductImages { get; set; }
        public IEnumerable<ProductVartionDTO> RelatedProductVartions { get; set; }

        
    }
}
