using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Order : EntityBase
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
