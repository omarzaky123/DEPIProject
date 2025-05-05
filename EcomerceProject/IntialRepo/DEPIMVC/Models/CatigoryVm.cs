using DEPIMVC.Repository;
using System.ComponentModel.DataAnnotations;

namespace DEPIMVC.Models
{
    public class CatigoryVm: IHasImage
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
