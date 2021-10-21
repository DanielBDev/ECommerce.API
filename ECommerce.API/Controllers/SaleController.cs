using ECommerce.API.Data;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Vendedor")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly AplicationDbContext _aplicationDbContext;

        public SaleController(ISaleService saleService, AplicationDbContext aplicationDbContext)
        {
            _saleService = saleService;
            _aplicationDbContext = aplicationDbContext;
        }

        [HttpPost("Add")]
        public ActionResult CreateSale([FromBody] SaleRequest sale)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _saleService.Add(sale, userId);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpPut("{Id}")]
        public ActionResult ModifySale([FromBody] SaleRequest sale)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _saleService.Modify(sale, userId);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteSale(int id)
        {
            if (ModelState.IsValid)
            {
                _saleService.Delete(id);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _saleService.GetAll();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public ActionResult GetSaleById(int id)
        {
            var sale = (from s in _aplicationDbContext.Sales
                         where s.Id == id

                         select new
                         {
                             s.Id,
                             s.Date,
                             s.UserId,
                             s.Total,
                         }).FirstOrDefault();

            var detailSale = (from ds in _aplicationDbContext.DetailSales
                               join p in _aplicationDbContext.Products on ds.ProductId equals p.Id
                               where ds.SaleId == id

                               select new
                               {
                                   ds.Id,
                                   ds.SaleId,
                                   ds.ProductId,
                                   productName = p.Title,
                                   ds.Price,
                                   ds.Quantity,
                                   ds.Total
                               }).ToList();

            return Ok(new { sale, detailSale });
        }
    }
}
