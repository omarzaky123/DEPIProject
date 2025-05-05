using DEPI.Core.Models;

namespace DEPIAPI.DTO
{
    public class GusetFullModelDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUserDTO User { get; set; }
    }
}
