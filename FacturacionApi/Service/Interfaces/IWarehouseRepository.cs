using Model.Models;
using System;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IWarehouseRepository
    {
        bool Add(Warehouse model);
        bool Update(int id, Warehouse model);
        Pagination<Warehouse> GetAll(int? page, Expression<Func<Warehouse, bool>> predicate);
        Warehouse Get(int Id);
        bool ValidateName(int WarehouseId, string name);
    }
}
