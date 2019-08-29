using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionApi.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int WarehouseId { get; set; }
        public bool Deleted { get; set; }
        public string WarehouseName { get; set; }
    }
}
