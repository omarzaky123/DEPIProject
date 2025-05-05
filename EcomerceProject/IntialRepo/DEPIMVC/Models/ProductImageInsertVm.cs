using DEPIMVC.Repository;
using System.ComponentModel.DataAnnotations;

namespace DEPIMVC.Models
{
    public class ProductImageInsertVm:IHasImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
    }
}
