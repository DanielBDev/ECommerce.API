using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface ISaleService
    {
        Sale Add(SaleRequest sale);
        Sale Modify(SaleRequest sale);
        Sale Delete(int id);
        IEnumerable<SaleResponse> GetAll();     
    }
}
