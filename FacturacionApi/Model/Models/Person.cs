using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Person : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        [StringLength(11)]
        public string DNI { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
    }
}
