using FacturacionApi.Dto;
using System.Collections.Generic;

namespace FacturacionApi.ViewModel
{
    public class WarehouseViewModel
    {
        public PaginationViewModel PaginationInfo { get; set; }
        public List<WarehouseDto> Items { get; set; }
    }
}
