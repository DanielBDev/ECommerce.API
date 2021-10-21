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
    [Authorize(Roles = "Admin, Deposito")]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _entryService;
        private readonly AplicationDbContext _aplicationDbContext;

        public EntryController(IEntryService entryService, AplicationDbContext aplicationDbContext)
        {
            _entryService = entryService;
            _aplicationDbContext = aplicationDbContext;
        }

        [HttpPost("Add")]
        public ActionResult CreateEntry([FromBody] EntryRequest entry)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _entryService.Add(entry, userId);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpPut("{Id}")]
        public ActionResult ModifyEntry([FromBody] EntryRequest entry)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _entryService.Modify(entry, userId);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteEntry(int id)
        {
            if (ModelState.IsValid)
            {
                _entryService.Delete(id);
                return Ok();
            }
            return new JsonResult("Algo salio mal") { StatusCode = 500 };
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _entryService.GetAll();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public ActionResult GetEntryById(int id)
        {
            var entry = (from e in _aplicationDbContext.Entries
                         where e.Id == id

                         select new
                         {
                             e.Id,
                             descriptionEntry = e.Description,
                             e.Date,
                             e.UserId,
                             e.Total,
                             e.ProviderId
                         }).FirstOrDefault();

            var detailEntry = (from de in _aplicationDbContext.DetailEntries
                               join p in _aplicationDbContext.Products on de.ProductId equals p.Id
                               where de.EntryId == id

                               select new
                               {
                                   de.Id,
                                   de.EntryId,
                                   de.ProductId,
                                   productName = p.Title,
                                   de.Cost,
                                   de.Quantity,
                                   de.Total
                               }).ToList();

            return Ok(new { entry, detailEntry });
        }
    }
}
