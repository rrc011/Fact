using FacturacionApi.Dto;
using System.Collections.Generic;

namespace FacturacionApi.ViewModel
{
    public class OrderViewModel
    {
        public PaginationViewModel PaginationInfo { get; set; }
        public List<OrderDto> Items { get; set; }
    }
}
