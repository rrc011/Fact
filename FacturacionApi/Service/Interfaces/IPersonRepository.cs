using Model.Models;
using System;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IPersonRepository
    {
        bool Add(Person model);
        bool Update(int id, Person model);
        Pagination<Person> GetAll(int? page, Expression<Func<Person, bool>> predicate);
        Pagination<Person> GetAll(int? page, Expression<Func<Person, bool>> predicate, int pageSize);
        Person Get(int Id);
        bool ValidateDNI(int PersonId, string dni);
    }
}
