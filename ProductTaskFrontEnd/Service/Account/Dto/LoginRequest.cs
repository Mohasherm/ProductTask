using System.ComponentModel.DataAnnotations;

namespace ProductTaskFrontEnd.Service.Account.Dto
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Password { get; set; }
    }

}