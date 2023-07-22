using System;
using System.ComponentModel.DataAnnotations;

namespace WeedTunes.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; set; }

        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
