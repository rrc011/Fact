using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionApi.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public int PersonId { get; set; }
    }
}
