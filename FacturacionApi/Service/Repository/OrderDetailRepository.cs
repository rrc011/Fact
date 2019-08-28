using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public OrderDetailRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(OrderDetail model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;

                _facturacionDbContext.OrderDetail.Add(model);
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
    }
}
