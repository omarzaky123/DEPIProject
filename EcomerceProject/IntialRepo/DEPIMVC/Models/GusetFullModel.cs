namespace DEPIMVC.Models
{
    public class GusetFullModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
