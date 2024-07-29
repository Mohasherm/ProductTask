using System;

namespace ProductTask.Repository.Account.Dto
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

    }
}
