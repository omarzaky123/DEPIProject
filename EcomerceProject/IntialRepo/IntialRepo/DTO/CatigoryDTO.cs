using System.ComponentModel.DataAnnotations;

namespace DEPIAPI.DTO
{
    public class CatigoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
