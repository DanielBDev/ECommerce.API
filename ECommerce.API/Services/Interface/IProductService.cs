using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface IProductService
    {
        Product Add(ProductRequest product, string userId);
        Product Modify(ProductRequest product, string userId);
        Product Delete(int id);
        IEnumerable<ProductResponse> GetAll();
        Product GetById(int id);
    }
}
