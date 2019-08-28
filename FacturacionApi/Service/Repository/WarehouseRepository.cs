using LinqKit;
using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Service.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public WarehouseRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(Warehouse model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;

                _facturacionDbContext.Warehouse.Add(model);
                _facturacionDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public Warehouse Get(int Id)
        {
            try
            {
                return _facturacionDbContext.Warehouse.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                return new Warehouse();
                throw;
            }
        }

        public Pagination<Warehouse> GetAll(int? page, Expression<Func<Warehouse, bool>> predicate)
        {
            try
            {
                var query = _facturacionDbContext.Warehouse.AsExpandable().Where(predicate).AsQueryable();
                return Pagination<Warehouse>.Create(query, page ?? 1, 5);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, Warehouse model)
        {
            try
            {
                var result = _facturacionDbContext.Warehouse.FirstOrDefault(x => x.Id == id);

                result.Name = string.IsNullOrEmpty(model.Name) ? result.Name : model.Name;
                result.Descripcion = string.IsNullOrEmpty(model.Descripcion) ? result.Descripcion : model.Descripcion;
                result.Deleted = model.Deleted;
                result.ModifiedDate = DateTime.Now;

                _facturacionDbContext.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _facturacionDbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public bool ValidateName(int WarehouseId, string name)
        {
            try
            {
                if (WarehouseId != 0)
                {
                    return _facturacionDbContext.Warehouse.Any(x => x.Name == name && x.Id == WarehouseId && !x.Deleted);
                }
                else
                {
                    return _facturacionDbContext.Warehouse.Any(x => x.Name == name && !x.Deleted);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
