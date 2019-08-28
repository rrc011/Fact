using FacturacionApi.Dto;
using System.Collections.Generic;

namespace FacturacionApi.ViewModel
{
    public class OrderDetailViewModel
    {
        public PaginationViewModel PaginationInfo { get; set; }
        public List<OrderDetailDto> Items { get; set; }
    }
}
