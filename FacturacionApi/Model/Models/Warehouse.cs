using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Warehouse : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Descripcion { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
