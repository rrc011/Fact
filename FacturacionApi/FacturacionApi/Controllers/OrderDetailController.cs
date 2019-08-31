using FacturacionApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;
using System;
using LinqKit;
using System.Collections.Generic;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IProductRepository productRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;
    }

        [HttpGet]
        [Route("/api/GetallOrderDetail/{orderId}")]
        public IActionResult GetAll(int orderId)
        {
            try
            {
                var predicate = PredicateBuilder.True<OrderDetail>();
                predicate = predicate.And(x => x.Deleted == false && x.OrderId == orderId);

                var model = orderDetailRepository.GetAll(predicate);
                var listProducts = new List<ProductDto>();

                foreach (var item in model)
                {
                    var x = productRepository.Get(item.ProductId);

                    listProducts.Add(new ProductDto()
                    {
                        ProductId = x.Id,
                        Name = x.Name,
                        Descripcion = x.Descripcion,
                        Stock = x.Stock,
                        Price = x.Price,
                        WarehouseName = x.Warehouse.Name,
                        WarehouseId = x.WarehouseId
                    });
                }

                return Ok(listProducts);
            }
            catch (Exception e)
            {
                throw e;
            }
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
        public IActionResult Post([FromBody] OrderDetailDto[] model)
        {
            try
            {
                var ordersDetails = new List<OrderDetail>();

                foreach (var item in model)
                {
                    var product = productRepository.Get(item.ProductId);

                    ordersDetails.Add(new OrderDetail()
                    {
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });

                    productRepository.Update(item.ProductId, new Product()
                    {
                        Id = item.ProductId,
                        Stock = product.Stock - item.Quantity
                    });
                }

                return Ok(orderDetailRepository.Add(ordersDetails.ToArray()));
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
