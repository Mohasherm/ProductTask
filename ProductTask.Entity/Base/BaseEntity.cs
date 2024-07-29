using System.ComponentModel.DataAnnotations;
using System;

namespace ProductTask.Entity.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsValid { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
