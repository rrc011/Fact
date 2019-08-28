using FacturacionApi.Dto;
using System.Collections.Generic;

namespace FacturacionApi.ViewModel
{
    public class PersonViewModel
    {
        public PaginationViewModel PaginationInfo { get; set; }
        public List<PersonDto> Items { get; set; }
    }
}
