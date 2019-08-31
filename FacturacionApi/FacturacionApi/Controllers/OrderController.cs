using DinkToPdf.Contracts;
using FacturacionApi.Dto;
using FacturacionApi.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interfaces;
using System;
using System.Linq;
using System.IO;
using DinkToPdf;
using FacturacionApi.Utility;
using Utilities;

namespace FacturacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private IConverter converter;

        public OrderController(IOrderRepository orderRepository, IConverter converter)
        {
            this.orderRepository = orderRepository;
            this.converter = converter;
        }

        [HttpGet]
        [Route("/api/GetallOrder/{page}/")]
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
                        Total = x.OrderDetails.Sum(e => e.Product.Price),
                        Date = x.CreatedDate.ToString("dd/MM/yyyy"),
                        OrderId = x.Id
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
                var result = orderRepository.Get(id);

                var model = new OrderDto()
                {
                    OrderId = result.Id,
                    PersonName = result.Person.Name + " " + result.Person.LastName
                };

                return Ok(model);
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

        [HttpGet]
        [Route("/api/GetPdfOrder/")]
        public IActionResult CreatePDF()
        {
            var predicate = PredicateBuilder.True<Order>();
            predicate = predicate.And(x => x.Deleted == false);

            var model = orderRepository.GetAll(predicate);

            var list = model.Select(x => new OrderDto()
            {
                Amount = x.Amount,
                PersonId = x.PersonId,
                PersonName = x.Person.Name + " " + x.Person.LastName,
                Total = x.OrderDetails.Sum(e => e.Product.Price),
                Date = x.CreatedDate.ToString("dd/MM/yyyy"),
                OrderId = x.Id
            }).ToList();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetOrderHTMLString(list),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            //var converter = new SynchronizedConverter(new PdfTools());

            var file = converter.Convert(pdf);
            return File(file, "application/pdf");
        }

        [HttpGet]
        [Route("/api/GetExcelOrder/")]
        public IActionResult CreateExcel()
        {
            var predicate = PredicateBuilder.True<Order>();
            predicate = predicate.And(x => x.Deleted == false);

            var model = orderRepository.GetAll(predicate);

            var list = model.Select(x => new 
            {
                Factura_Nº = x.Id,
                Cliente = x.Person.Name + " " + x.Person.LastName,
                Fecha = x.CreatedDate.ToString("dd/MM/yyyy"),
                Total = x.Amount,
            }).ToList();

            var fileName = $"test.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] fileBytes = ExcelManagement.ListToExcel(list);
            return File(fileBytes, mimeType, fileName);
        }
    }
}
