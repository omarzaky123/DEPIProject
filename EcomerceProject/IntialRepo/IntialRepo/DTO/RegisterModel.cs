using System.ComponentModel.DataAnnotations;

namespace DEPIAPI.DTO
{
    public class RegisterModel
    {

        [MinLength(1, ErrorMessage = "The name must be greater than 1")]
        [Required(ErrorMessage = "The field is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [MinLength(1, ErrorMessage = "The address must be greater than 1")]
        public string Address { get; set; }
    }
}
