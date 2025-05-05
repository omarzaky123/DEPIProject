using System.ComponentModel.DataAnnotations;

namespace DEPIMVC.Models
{
    public class ProductVm
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
        public IEnumerable<ProductVm> RelatedProduct { get; set; }
        public IEnumerable<ProductVationVm> RelatedProductVartions { get; set; }
        public IEnumerable<ProductImageInsertVm> ProductImages { get; set; }
        public VartionInsertVm Vartion { get; set; }

    }
}
