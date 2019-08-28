using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
