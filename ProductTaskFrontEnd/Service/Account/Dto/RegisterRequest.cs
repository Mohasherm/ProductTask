﻿using System.ComponentModel.DataAnnotations;

namespace ProductTaskFrontEnd.Service.Account.Dto
{
    public class RegisterRequest
    {
        [Required(ErrorMessage =  "هذا الحقل مطلوب")]
        public string UserName { get; set; }
        [Required(ErrorMessage =  "هذا الحقل مطلوب")]
        public string Password { get; set; }
        [Required(ErrorMessage =  "هذا الحقل مطلوب")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage =  "هذا الحقل مطلوب")]
        public string Email { get; set; }

    }
}