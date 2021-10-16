using ECommerce.API.Data;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AplicationDbContext _applicationDbContext;

        public ProductController(IProductService productService, AplicationDbContext aplicationDbContext)
        {
            _productService = productService;

            _applicationDbContext = aplicationDbContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("Code")]
        public ActionResult<string> GetCode(string productCode)
        {
            var result = _productService.GetByCode(productCode);
            return Ok(result);
        }

        [HttpGet("Title")]
        public ActionResult<string> GetTitle(string productTitle)
        {
            var result = _productService.GetByTitle(productTitle);
            return Ok(result);
        }

        [HttpPost("Add")]
        public ActionResult CreateProduct([FromBody] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpPut("{Id}")]
        public ActionResult ModifyProduct([FromBody] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                _productService.Modify(product);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                _productService.Delete(id);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpGet("{Id}")]
        public ActionResult<int> GetStock(int id)
        {
            var pStock = _applicationDbContext.Products.FirstOrDefault(p => p.Id == id);

            int stock = (int)pStock.Stock;

            return stock;
        }
    }
}
