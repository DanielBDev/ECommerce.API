using ECommerce.API.Data;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly AplicationDbContext _applicationDbContext;

        public ProviderController(AplicationDbContext aplicationDbContext, IProviderService providerService)
        {
            _applicationDbContext = aplicationDbContext;
            _providerService = providerService;
        }

        [HttpPost("Add")]
        public ActionResult CreateProvider([FromBody] ProviderRequest provider)
        {
            if (ModelState.IsValid)
            {
                _providerService.Add(provider);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpPut("{Id}")]
        public ActionResult ModifyProvider([FromBody] ProviderRequest provider)
        {
            if (ModelState.IsValid)
            {
                _providerService.Modify(provider);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteProvider(int id)
        {
            if (ModelState.IsValid)
            {
                _providerService.Delete(id);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _providerService.GetAll();
            return Ok(result);
        }
    }
}
