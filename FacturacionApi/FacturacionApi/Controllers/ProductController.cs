using System;
using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        [Route("/api/GetallProduct/{page}/")]
        public IActionResult Get(int? page, string search)
        {
            try
            {
                var predicate = PredicateBuilder.True<Product>();
                predicate = predicate.And(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(search))
                    predicate = predicate.And(x => (x.Name.Contains(search) || x.Descripcion.Contains(search)));

                var model = productRepository.GetAll(page, predicate);

                var outputmodel = new ProductViewModel()
                {
                    PaginationInfo = new PaginationViewModel()
                    {
                        HasNextPage = model.HasNextPage,
                        HasPreviousPage = model.HasPreviousPage,
                        PageIndex = model.PageIndex,
                        TotalPages = model.TotalPages,
                        TRecords = model.TRecords
                    },

                    Items = model.Select(x => new ProductDto()
                    {
                       Name = x.Name,
                       Descripcion = x.Descripcion,
                       Price = x.Price,
                       Stock = x.Stock,
                       WarehouseId = x.WarehouseId
                    }).ToList()
                };

                return Ok(outputmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: api/Product/5
        [Route("/api/GetProduct/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(productRepository.Get(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto model)
        {
            try
            {
                return Ok(
                        productRepository.Add(new Product()
                        {
                            Name = model.Name,
                            Descripcion = model.Descripcion,
                            Price = model.Price,
                            Stock = model.Stock,
                            WarehouseId = model.WarehouseId
                        })
                    );
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDto model)
        {
            try
            {
                return Ok(
                         productRepository.Update(id, new Product()
                         {
                             Id = model.ProductId,
                             Name = model.Name,
                             Descripcion = model.Descripcion,
                             Deleted = model.Deleted,
                             Price = model.Price,
                             Stock = model.Stock,
                             WarehouseId = model.WarehouseId,
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
                return Ok(productRepository.Update(id, new Product()
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
