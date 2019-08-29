using FacturacionApi.Dto;
using FacturacionApi.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;
using System;
using System.Linq;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("/api/GetallOrder/{page}/{orderId}")]
        public IActionResult Get(int? page)
        {
            try
            {
                var predicate = PredicateBuilder.True<Order>();
                predicate = predicate.And(x => x.Deleted == false);

                var model = orderRepository.GetAll(page, predicate);

                var outputmodel = new OrderViewModel()
                {
                    PaginationInfo = new PaginationViewModel()
                    {
                        HasNextPage = model.HasNextPage,
                        HasPreviousPage = model.HasPreviousPage,
                        PageIndex = model.PageIndex,
                        TotalPages = model.TotalPages,
                        TRecords = model.TRecords
                    },

                    Items = model.Select(x => new OrderDto()
                    {
                        Amount = x.Amount,
                        PersonId = x.PersonId,
                        PersonName = x.Person.Name + " " + x.Person.LastName,
                        Total = x.OrderDetails.Sum(e => e.Product.Price)
                    }).ToList()
                };

                return Ok(outputmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("/api/GetOrder/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(orderRepository.Get(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderDto model)
        {
            try
            {
                return Ok(
                        orderRepository.Add(new Order()
                        {
                            Amount = model.Amount,
                            PersonId = model.PersonId
                        })
                    );
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDto model)
        {
            try
            {
                return Ok(
                         orderRepository.Update(id, new Order()
                         {
                             Id = model.OrderId,
                             Amount = model.Amount,
                             Deleted = model.Deleted,
                             PersonId = model.PersonId
                         })
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(orderRepository.Update(id, new Order()
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
    }
}
