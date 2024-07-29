using System;

namespace ProductTask.Repository.Permission.Dto
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
    }
}
