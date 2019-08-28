using FacturacionApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionApi.ViewModel
{
    public class ProductViewModel
    {
        public PaginationViewModel PaginationInfo { get; set; }
        public List<ProductDto> Items { get; set; }
    }
}
