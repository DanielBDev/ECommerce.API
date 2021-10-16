using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CashRegisterController : ControllerBase
    {
        private readonly ICashRegisterService _cashRegisterService;

        public CashRegisterController(ICashRegisterService cashRegisterService)
        {
            _cashRegisterService = cashRegisterService;
        }

        //[HttpPost("{cashRegister}/{user}")]
        [HttpPost("Open")]
        public ActionResult OpenCashRegister([FromBody] CashRegisterRequest cashRegister, IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                _cashRegisterService.OpenCashRegister(cashRegister, user);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }
    }
}
