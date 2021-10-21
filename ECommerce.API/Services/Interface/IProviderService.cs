using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Services.Interface
{
    public interface IProviderService
    {
        Provider Add(ProviderRequest provider);
        Provider Modify(ProviderRequest provider);
        Provider Delete(int id);
        IEnumerable<ProviderResponse> GetAll();
        Provider GetById(int id);
    }
}
