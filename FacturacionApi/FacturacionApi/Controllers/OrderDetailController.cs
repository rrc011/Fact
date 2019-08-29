using FacturacionApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;
using System;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }

        [Route("/api/GetOrderDetail/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(orderDetailRepository.Get(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] OrderDetailDto model)
        {
            try
            {
                return Ok(
                        orderDetailRepository.Add(new OrderDetail()
                        {
                            OrderId = model.OrderId,
                            ProductId = model.ProductId,
                            Quantity = model.Quantity
                        })
                    );
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
