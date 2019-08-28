using System;
using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.OutPutModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //// GET: api/Categoria
        //[HttpGet]
        //[Route("/api/Getall/{page}/")]
        //public IActionResult GetAll(int? page, string search)
        //{
        //    try
        //    {
        //        var model = _categoriaService.GetAll(page, search);

        //        var OutPutModel = new CategoriaOutPutModel()
        //        {
        //            PaginationInfo = new PaginationOutPutModel()
        //            {
        //                HasNextPage = model.HasNextPage,
        //                HasPreviousPage = model.HasPreviousPage,
        //                PageIndex = model.PageIndex,
        //                TotalPages = model.TotalPages,
        //                TRecords = model.TRecords
        //            },

        //            Items = model.Select(x => new CategoriaDto()
        //            {
        //                CategoriaId = x.CategoriaId,
        //                Descripcion = x.Descripcion,
        //                EstadoReg = x.EstadoReg,
        //                Nombre = x.Nombre
        //            }).ToList()
        //        };

        //        return Ok(OutPutModel);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //// GET: api/Categoria/5
        //[HttpGet("/api/getcategoria/{id}")]
        //public IActionResult Get(int id)
        //{
        //    try
        //    {
        //        return Ok(_categoriaService.Get(id));
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}

        //// POST: api/Categoria
        //[HttpPost]
        //public IActionResult Post([FromBody] CategoriaDto model)
        //{
        //    try
        //    {
        //        return Ok(
        //                _categoriaService.Add(
        //                        new Model.Models.Categoria()
        //                        {
        //                            Descripcion = model.Descripcion,
        //                            Nombre = model.Nombre
        //                        }
        //                    )
        //            );
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}

        //// PUT: api/Categoria/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] CategoriaDto model)
        //{
        //    try
        //    {
        //        return Ok(
        //                 _categoriaService.Update(id, new Model.Models.Categoria()
        //                 {
        //                     Descripcion = model.Descripcion,
        //                     Nombre = model.Nombre,
        //                     EstadoReg = model.EstadoReg
        //                 })
        //            );
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        return Ok(_categoriaService.Update(id, new Model.Models.Categoria()
        //        {
        //            CategoriaId = id,
        //            EstadoReg = false
        //        }));
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}

        //[HttpGet]
        //public IActionResult validateName(int Id, string name)
        //{
        //    try
        //    {
        //        return Ok(_categoriaService.ValidateCategoria(Id, name));
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}
    }
}
