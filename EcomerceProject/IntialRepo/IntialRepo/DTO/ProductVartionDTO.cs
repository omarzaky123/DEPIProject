using DEPI.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPIAPI.DTO
{
    public class ProductVartionDTO
    {
        public int Id { get; set; }
        public string VartionValue { get; set; }
        public decimal AddtionalPrice { get; set; }
        public int Quantity_In_Stock { get; set; }
        public int ProductID { get; set; }
        public int? VartionID { get; set; }

        public ProductDTO Product { get; set; }
    }
}
