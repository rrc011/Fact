using LinqKit;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Service.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public ProductRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(Product model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;

                _facturacionDbContext.Product.Add(model);
                _facturacionDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public Product Get(int Id)
        {
            try
            {
                return _facturacionDbContext.Product.Include(x => x.Warehouse).FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                return new Product();
                throw;
            }
        }

        public Pagination<Product> GetAll(int? page, Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var query = _facturacionDbContext.Product.Include(x => x.Warehouse)
                                                         .AsExpandable().Where(predicate).AsQueryable();

                return Pagination<Product>.Create(query, page ?? 1, 5);
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, Product model)
        {
            try
            {
                var result = _facturacionDbContext.Product.FirstOrDefault(x => x.Id == id);

                result.Name = string.IsNullOrEmpty(model.Name) ? result.Name : model.Name;
                result.Descripcion = string.IsNullOrEmpty(model.Descripcion) ? result.Descripcion : model.Descripcion;
                result.Price = model.Price == 0 ? result.Price : model.Price;
                result.Stock = model.Stock == 0 ? result.Stock : model.Stock;
                result.WarehouseId = model.WarehouseId == 0 ? result.WarehouseId : model.WarehouseId;
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

        public bool ValidateName(int ProductId, string name)
        {
            try
           {
                if (ProductId != 0)
                {
                    return _facturacionDbContext.Product.Any(x => x.Name == name && x.Id != ProductId && !x.Deleted);
                }
                else
                {
                    return _facturacionDbContext.Product.Any(x => x.Name == name && !x.Deleted);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
