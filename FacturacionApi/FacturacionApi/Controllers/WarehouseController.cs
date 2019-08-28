using System;
using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Model.Models;
using LinqKit;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        // GET: api/Warehouse
        [HttpGet]
        [Route("/api/GetallWarehouse/{page}/")]
        public IActionResult Get(int? page, string search)
        {
            try
            {
                var predicate = PredicateBuilder.True<Warehouse>();
                predicate = predicate.And(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(search))
                    predicate = predicate.And(x => (x.Name.Contains(search) || x.Descripcion.Contains(search)));

                var model = warehouseRepository.GetAll(page, predicate);

                var outputmodel = new WarehouseViewModel()
                {
                    PaginationInfo = new PaginationViewModel()
                    {
                        HasNextPage = model.HasNextPage,
                        HasPreviousPage = model.HasPreviousPage,
                        PageIndex = model.PageIndex,
                        TotalPages = model.TotalPages,
                        TRecords = model.TRecords
                    },

                    Items = model.Select(x => new WarehouseDto()
                    {
                        WarehouseId = x.Id,
                        Name = x.Name,
                        Descripcion = x.Descripcion,
                        Location = x.Location
                    }).ToList()
                };

                return Ok(outputmodel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}", Name = "Get")]
        [HttpGet("/api/GetWarehouse/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(warehouseRepository.Get(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // POST: api/Warehouse
        [HttpPost]
        public IActionResult Post([FromBody] WarehouseDto model)
        {
            try
            {
                return Ok(
                        warehouseRepository.Add(new Warehouse()
                        {
                            Name = model.Name,
                            Descripcion = model.Descripcion,
                            Location = model.Location
                        })
                    );
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // PUT: api/Warehouse/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] WarehouseDto model)
        {
            try
            {
                return Ok(
                         warehouseRepository.Update(id, new Warehouse()
                         {
                             Id = model.WarehouseId,
                             Name = model.Name,
                             Descripcion = model.Descripcion,
                             Deleted = model.Deleted
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
                return Ok(warehouseRepository.Update(id, new Warehouse()
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
