using ProductTask.Entity.Base;
using ProductTask.Entity.Security;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Permission
{
    [Table("WebContentRoles", Schema = "Permission")]
    public class WebContentRoleModel : BaseEntity
    {
        public bool CanDelete { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }

        public WebContentModel WebContent { get; set; }
        public Guid WebContentId { get; set; }

        public RoleModel Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
