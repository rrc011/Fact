using System;
using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        // GET: api/Person
        [HttpGet]
        [Route("/api/GetallPerson/{page}/")]
        public IActionResult Get(int? page, string search)
        {
            try
            {
                var predicate = PredicateBuilder.True<Person>();
                predicate = predicate.And(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(search))
                    predicate = predicate.And(x => (x.Name.Contains(search) || x.LastName.Contains(search) || x.DNI.Contains(search)));

                var model = personRepository.GetAll(page, predicate);

                var outputmodel = new PersonViewModel()
                {
                    PaginationInfo = new PaginationViewModel()
                    {
                        HasNextPage = model.HasNextPage,
                        HasPreviousPage = model.HasPreviousPage,
                        PageIndex = model.PageIndex,
                        TotalPages = model.TotalPages,
                        TRecords = model.TRecords
                    },

                    Items = model.Select(x => new PersonDto()
                    {
                        Name = x.Name,
                        LastName = x.LastName,
                        DNI = x.DNI,
                        Address = x.Address,
                        Email = x.Email,
                        Gender = x.Gender,
                        Phone = x.Phone,
                        PersonId = x.Id,
                        GenderName = x.Gender == 1 ? "Masculino" : "Femenino"
                    }).ToList()
                };

                return Ok(outputmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: api/Person/5
        [Route("/api/GetPerson/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = personRepository.Get(id);

                var outPutModel = new PersonDto()
                {
                    PersonId = result.Id,
                    Address = result.Address,
                    DNI = result.DNI,
                    Email = result.Email,
                    Gender = result.Gender,
                    LastName = result.LastName,
                    Name = result.Name,
                    Phone = result.Phone,
                    GenderName = result.Gender == 1 ? "Masculino" : "Femenino"
                };

                return Ok(outPutModel);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Route("/api/ValidateDNI/{personId}/{dni}")]
        public IActionResult Get(int personId, string dni)
        {
            try
            {
                var result = personRepository.ValidateDNI(personId, dni);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: api/Person
        [HttpPost]
        public IActionResult Post([FromBody] PersonDto model)
        {
            try
            {
                return Ok(
                        personRepository.Add(new Person()
                        {
                            Name = model.Name,
                            LastName = model.LastName,
                            DNI = model.DNI,
                            Address = model.Address,
                            Email = model.Email,
                            Gender = model.Gender,
                            Phone = model.Phone
                        })
                    );
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonDto model)
        {
            try
            {
                return Ok(
                         personRepository.Update(id, new Person()
                         {
                             Id = model.PersonId,
                             Name = model.Name,
                             LastName = model.LastName,
                             Deleted = model.Deleted,
                             Address = model.Address,
                             DNI = model.DNI,
                             Email = model.Email,
                             Gender = model.Gender,
                             Phone = model.Phone
                         })
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(personRepository.Update(id, new Person()
                {
                    Id = id,
                    Deleted = true
                }));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Route("/api/LoadSelectPerson/")]
        public IActionResult Get()
        {
            try
            {
                var predicate = PredicateBuilder.True<Person>();
                predicate = predicate.And(x => x.Deleted == false);

                var model = personRepository.GetAll(1, predicate, 1000);

                var outputmodel = model.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name + " " + u.LastName,
                    Value = u.Id.ToString()
                });

                return Ok(outputmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
