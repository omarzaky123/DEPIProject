using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class VartionWithCatDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int CatigoryId { get; set; }
        public virtual CatigoryDTO Catigory { get; set; }

        public virtual ICollection<ProductVartionDTO> ProductVartions { get; set; }

    }
}
