using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.OutPutModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoServicioController : ControllerBase
    {
        private readonly IProductoServicioService _productoServicioService;
        public ProductoServicioController(IProductoServicioService productoServicioService)
        {
            _productoServicioService = productoServicioService;
        }

        //// GET: api/ProductoServicio
        //[HttpGet]
        //[Route("/api/getallpd/{tipo}/{page}/")]
        //public IActionResult GetAll(int? page, int tipo, string search)
        //{
        //    var model = _productoServicioService.GetAll(page, tipo, search);

        //    var outPutModel = new ProductoServicioOutPutModel()
        //    {
        //        PaginationInfo = new PaginationOutPutModel()
        //        {
        //            PageIndex = model.PageIndex,
        //            HasNextPage = model.HasNextPage,
        //            HasPreviousPage = model.HasPreviousPage,
        //            TotalPages = model.TotalPages,
        //            TRecords = model.TRecords
        //        },

        //        Items = model.Select(x => new ProductoServicioDto()
        //        {
        //            Estado = x.Estado,
        //            EstadoReg = x.EstadoReg,
        //            Nombre = x.Nombre,
        //            PrecioCompra = x.PrecioCompra,
        //            PrecioVenta = x.PrecioVenta,
        //            Stock = x.Stock,
        //            Tipo = x.Tipo,
        //            ProductoServicioId = x.ProductoServicioId
        //        }).ToList()
        //    };

        //    return Ok(outPutModel);
        //}

        //// GET: api/ProductoServicio/5
        //[HttpGet]
        //[Route("/api/getpd/{id}")]
        //public IActionResult Get(int id)
        //{
        //    return Ok(_productoServicioService.Get(id));
        //}

        //// POST: api/ProductoServicio
        //[HttpPost]
        //public IActionResult Post([FromBody] ProductoServicioDto model)
        //{
        //    return Ok(
        //        _productoServicioService.Add(new Model.Models.ProductoServicio()
        //        {
        //            Nombre = model.Nombre,
        //            Stock = model.Stock,
        //            Tipo = model.Tipo,
        //            CategoriaId = model.CategoriaId,
        //            PrecioVenta = model.PrecioVenta,
        //            PrecioCompra = model.PrecioCompra,
        //            Estado = model.Estado,
        //            Imagen = string.IsNullOrEmpty(model.Imagen) ? FileHelper.FileToByteArray("/Assets/Images/production-icon-default.png") :
        //                     FileHelper.FileToByteArray(model.Imagen)
        //        })
        //        );; ;
        //}

        //// PUT: api/ProductoServicio/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] ProductoServicioDto model)
        //{
        //    return Ok(
        //            _productoServicioService.Update(id, new Model.Models.ProductoServicio()
        //            {
        //                CategoriaId = model.CategoriaId,
        //                Stock = model.Stock,
        //                ProductoServicioId = model.ProductoServicioId,
        //                Estado = model.Estado,
        //                Imagen = string.IsNullOrEmpty(model.Imagen) ? null : FileHelper.FileToByteArray(model.Imagen),
        //                Nombre = model.Nombre,
        //                PrecioCompra = model.PrecioCompra,
        //                PrecioVenta = model.PrecioVenta,
        //                Tipo = model.Tipo
        //            })
        //        ); ;
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return Ok(
        //            _productoServicioService.Update(id, new Model.Models.ProductoServicio()
        //            {
        //                ProductoServicioId = id,
        //                EstadoReg = false
        //            })
        //        );
        //}

        //[HttpGet]
        //public IActionResult ValidatePS(int id, string nombre)
        //{
        //    return Ok(_productoServicioService.ValidatePS(id, nombre));
        //}
    }
}
