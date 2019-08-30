using System;

namespace FacturacionApi.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public int PersonId { get; set; }
        public string Date { get; set; }
        public decimal Total { get; set; }
        public string PersonName { get; set; }
        public bool Deleted { get; set; }
    }
}
