using ProductTask.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Security
{
    [Table("Roles", Schema = "Security")]
    public class RoleModel : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserModel> Users { get; set; }
    }
}
