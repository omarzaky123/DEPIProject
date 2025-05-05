using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class VartionInsertDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatigoryId { get; set; }
    }
}
