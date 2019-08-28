using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Product : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
