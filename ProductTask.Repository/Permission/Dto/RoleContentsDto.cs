using System;
using System.Collections.Generic;

namespace ProductTask.Repository.Permission.Dto
{
    public class RoleContentsDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ContentDto> Contents { get; set; }
    }
}
