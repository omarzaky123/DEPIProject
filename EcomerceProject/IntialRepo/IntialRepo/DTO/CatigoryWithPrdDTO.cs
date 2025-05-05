using DEPI.Core.Models;

namespace DEPIAPI.DTO
{
    public class CatigoryWithPrdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }

    }
}
