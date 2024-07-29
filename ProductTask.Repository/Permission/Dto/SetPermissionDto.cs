using System;
using System.Collections.Generic;

namespace ProductTask.Repository.Permission.Dto
{
    public class SetPermissionDto
    {
        public SetPermissionDto()
        {
            Contents = new List<ContentDto>();
        }
        public Guid RoleId { get; set; }
        public List<ContentDto> Contents { get; set; }
    }
}
