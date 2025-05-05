namespace DEPIMVC.Models
{
    public class CatigoryWithPrdsVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public virtual ICollection<ProductVm> Products { get; set; }
    }
}
