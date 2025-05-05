using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Core.Models
{
    public class Vartion
    {

        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Catigory")]
        public int CatigoryId { get; set; }
        public virtual Catigory Catigory { get; set; }

        public virtual ICollection<ProductVartion> ProductVartions { get; set; }

    }
}
