using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IOrderDetailRepository
    {
        bool Add(OrderDetail[] model);
        OrderDetail Get(int Id);
        List<OrderDetail> GetAll(Expression<Func<OrderDetail, bool>> expression);
    }
}
