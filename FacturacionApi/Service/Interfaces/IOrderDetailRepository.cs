using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IOrderDetailRepository
    {
        bool Add(OrderDetail model);
        OrderDetail Get(int Id);
    }
}
