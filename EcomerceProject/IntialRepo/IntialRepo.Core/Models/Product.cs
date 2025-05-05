using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }

        [ForeignKey("Catigory")]
        public int CatigoryId { get; set; }

        public virtual Catigory Catigory { get;set; }   
        public virtual ICollection<ProductVartion> ProductVartions { get;set; }
        public virtual ICollection<ProductImage> ProductImages { get;set; }

    }
}
