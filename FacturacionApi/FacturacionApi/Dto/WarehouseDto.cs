﻿namespace FacturacionApi.Dto
{
    public class WarehouseDto
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Location { get; set; }
        public bool Deleted { get; set; }
    }
}
