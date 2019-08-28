using System.Linq;
using FacturacionApi.Dto;
using FacturacionApi.OutPutModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        //// GET: api/Persona
        //[HttpGet]
        //[Route("/api/getall/{tipo}/{page}/")]
        //public IActionResult GetAll(int? page, int tipo, string search)
        //{
        //    var model =  _personaService.GetAll(page, tipo, search);

        //    var outPutModel = new PersonaOutPutModel()
        //    {
        //        PaginationInfo = new PaginationOutPutModel()
        //        {
        //            PageIndex = model.PageIndex,
        //            HasNextPage = model.HasNextPage,
        //            HasPreviousPage = model.HasPreviousPage,
        //            TotalPages = model.TotalPages,
        //            TRecords = model.TRecords
        //        },

        //        Items = model.Select(x => new PersonaDto()
        //        {
        //            Nombres = x.Nombres,
        //            Apellidos = x.Apellidos,
        //            Correo = x.Correo,
        //            Direccion = x.Direccion,
        //            EstadoReg = x.EstadoReg,
        //            PersonaId = x.PersonaId,
        //            Telefono = x.Telefono,
        //            Tipo = x.Tipo,
        //            Cedula = x.Cedula
        //        }).ToList()
        //    };

        //    return Ok(outPutModel);
        //}

        //// GET: api/Persona/5
        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    return Ok(_personaService.Get(id));
        //}

        //// POST: api/Persona
        //[HttpPost]
        //public IActionResult Post([FromBody] PersonaDto model)
        //{
        //    return Ok(
        //        _personaService.Add(new Model.Models.Persona()
        //        {
        //            Nombres = model.Nombres,
        //            Apellidos = model.Apellidos,
        //            Correo = model.Correo,
        //            Direccion = model.Direccion,
        //            Telefono = model.Telefono,
        //            Tipo = model.Tipo,
        //            Cedula = model.Cedula
        //        })
        //        ); ;
        //}

        //// PUT: api/Persona/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] PersonaDto model)
        //{
        //    return Ok(
        //            _personaService.Update(id, new Model.Models.Persona()
        //            {
        //                PersonaId = model.PersonaId,
        //                Nombres = model.Nombres,
        //                Apellidos = model.Apellidos,
        //                Correo = model.Correo,
        //                Direccion = model.Direccion,
        //                EstadoReg = model.EstadoReg,
        //                Telefono = model.Telefono,
        //                Tipo = model.Tipo,
        //                Cedula = model.Cedula
        //            })
        //        );
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return Ok(
        //            _personaService.Update(id, new Model.Models.Persona()
        //            {
        //                PersonaId = id,
        //                EstadoReg = false
        //            })
        //        );
        //}

        //[HttpGet]
        //public IActionResult ValidateDNI(string cedula)
        //{
        //    return Ok(_personaService.ValidateDNI(cedula));
        //}
    }
}
