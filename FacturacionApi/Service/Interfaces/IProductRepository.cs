using Model.Models;
using System;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IProductRepository
    {
        bool Add(Product model);
        bool Update(int id, Product model);
        Pagination<Product> GetAll(int? page, Expression<Func<Product, bool>> predicate);
        Product Get(int Id);
        bool ValidateName(int ProductId, string name);
    }
}
