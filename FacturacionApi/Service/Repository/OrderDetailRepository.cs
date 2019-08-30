using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Service.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public OrderDetailRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(OrderDetail[] model)
        {
            try
            {
                foreach (var item in model)
                {
                    item.CreatedDate = DateTime.Now;
                    item.Deleted = false;
                }

                _facturacionDbContext.OrderDetail.AddRange(model);
                _facturacionDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public OrderDetail Get(int Id)
        {
            try
            {
                return _facturacionDbContext.OrderDetail.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                return new OrderDetail();
                throw;
            }
        }

        public List<OrderDetail> GetAll(Expression<Func<OrderDetail, bool>> expression)
        {
            try
            {
                return _facturacionDbContext.OrderDetail.Include(x => x.Product).AsExpandable()
                                                        .Where(expression).ToList();
            }
            catch (Exception)
            {
                return new List<OrderDetail>();
                throw;
            }
        }
    }
}
