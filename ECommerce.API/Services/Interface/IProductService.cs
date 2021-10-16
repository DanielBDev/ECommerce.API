using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface IProductService
    {
        Product Add(ProductRequest product);
        Product Modify(ProductRequest product);
        Product Delete(int id);

        IEnumerable<ProductResponse> GetAll();

        IEnumerable<ProductRequest> GetByTitle(string title);

        IEnumerable<ProductRequest> GetByCode(string code);

        ProductRequest GetById(int id);
    }
}
