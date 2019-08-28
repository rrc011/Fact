using LinqKit;
using Model.Models;
using Persistence;
using Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Service.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly FacturacionDbContext _facturacionDbContext;
        public PersonRepository(FacturacionDbContext facturacionDbContext)
        {
            _facturacionDbContext = facturacionDbContext;
        }

        public bool Add(Person model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;

                _facturacionDbContext.Person.Add(model);
                _facturacionDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public Person Get(int Id)
        {
            try
            {
                return _facturacionDbContext.Person.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                return new Person();
                throw;
            }
        }

        public Pagination<Person> GetAll(int? page, /*string search,*/ Expression<Func<Person, bool>> predicate)
        {
            try
            {
                var query = _facturacionDbContext.Person.AsExpandable().Where(predicate).AsQueryable();

                return Pagination<Person>.Create(query, page ?? 1, 5);

                //var predicate = LinqKit.PredicateBuilder.True<Person>();
                //predicate = predicate.And(x => x.Deleted == false);

                //if (!string.IsNullOrEmpty(search))
                //{
                //    predicate = predicate.And(x => (x.Name.Contains(search) || x.LastName.Contains(search) || x.DNI.Contains(search)));
                //    var query = _facturacionDbContext.Person.AsExpandable().Where(predicate).AsQueryable();

                //    return Pagination<Person>.Create(query, page ?? 1, 5);
                //}
                //else
                //{
                //    var query = _facturacionDbContext.Person.AsExpandable().Where(predicate).AsQueryable();

                //    return Pagination<Person>.Create(query, page ?? 1, 5);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, Person model)
        {
            try
            {
                var result = _facturacionDbContext.Person.FirstOrDefault(x => x.Id == id);

                result.Name = string.IsNullOrEmpty(model.Name) ? result.Name : model.Name;
                result.LastName = string.IsNullOrEmpty(model.LastName) ? result.LastName : model.LastName;
                result.Phone = string.IsNullOrEmpty(model.Phone) ? result.Phone : model.Phone;
                result.Email = string.IsNullOrEmpty(model.Email) ? result.Email : model.Email;
                result.Address = string.IsNullOrEmpty(model.Address) ? result.Address : model.Address;
                result.Gender = model.Gender == 0 ? result.Gender : model.Gender;
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

        public bool ValidateEmail(int PersonId, string name)
        {
            try
            {
                if (PersonId != 0)
                {
                    return _facturacionDbContext.Person.Any(x => x.Name == name && x.Id == PersonId && !x.Deleted);
                }
                else
                {
                    return _facturacionDbContext.Person.Any(x => x.Name == name && !x.Deleted);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
