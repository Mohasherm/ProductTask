using ProductTask.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Permission
{
    [Table("WebContents", Schema = "Permission")]
    public class WebContentModel : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<WebContentRoleModel> WebContentRoles { get; set; }
    }
}
