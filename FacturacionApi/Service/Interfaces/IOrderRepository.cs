﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Service.Interfaces
{
    public interface IOrderRepository
    {
        bool Add(Order model);
        bool Update(int id, Order model);
        Pagination<Order> GetAll(int? page, Expression<Func<Order, bool>> expression);
        Order Get(int Id);
    }
}
