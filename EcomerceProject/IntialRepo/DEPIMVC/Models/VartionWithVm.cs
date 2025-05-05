using System.ComponentModel.DataAnnotations;

namespace DEPIMVC.Models
{
    public class VartionWithVm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CatigoryId { get; set; }
        public virtual CatigoryVm Catigory { get; set; }

        public virtual List<ProductVationVm> ProductVartions { get; set; }
    }
}
