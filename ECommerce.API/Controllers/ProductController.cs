using ECommerce.API.Data;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
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

        [HttpPost("Add")]
        public ActionResult CreateProduct([FromBody] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _productService.Add(product, userId);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpPut("{Id}")]
        public ActionResult ModifyProduct([FromBody] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _productService.Modify(product, userId);
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
