using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public OrderRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(Order model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;

                _facturacionDbContext.Order.Add(model);
                _facturacionDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public Order Get(int Id)
        {
            try
            {
                return _facturacionDbContext.Order.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                return new Order();
                throw;
            }
        }

        public Pagination<Order> GetAll(int? page)
        {
            try
            {
                var query = _facturacionDbContext.Order.AsQueryable();

                return Pagination<Order>.Create(query, page ?? 1, 5);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, Order model)
        {
            try
            {
                var result = _facturacionDbContext.Order.FirstOrDefault(x => x.Id == id);

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
    }
}
