using ProductTask.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Security
{
    [Table("Users", Schema = "Security")]
    public class UserModel : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public RoleModel Role { get; set; }

    }
}
