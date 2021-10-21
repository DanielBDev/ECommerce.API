using ECommerce.API.Data;
using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using ECommerce.API.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public ProductService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public Product Add(ProductRequest product, string userId)
        {
            var entity = new Product
            {
                Title = product.Title,
                Code = product.Code,
                Date = DateTime.Now,
                Price = product.Price,
                Cost = 0,
                Stock = 0,
                State = true,
                UserId = userId
            };

            _aplicationDbContext.Add(entity);
            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Product Modify(ProductRequest product, string userId)
        {
            var entity = _aplicationDbContext.Products.FirstOrDefault(x => x.Id == product.Id);

            entity.Code = product.Code;
            entity.Title = product.Title;
            entity.Cost = product.Cost;
            entity.Price = product.Price;
            entity.Stock = product.Stock;
            entity.Date = DateTime.Now;
            entity.State = true;
            entity.UserId = userId;

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Product Delete(int id)
        {
            var entity = _aplicationDbContext.Products.FirstOrDefault(x => x.Id == id);

            entity.State = false;

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<ProductResponse> GetAll()
        {
            return _aplicationDbContext.Products.Where(p => p.State == true).Select(p => new ProductResponse
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                Price = p.Price,
                Cost = (decimal)p.Cost,
                Stock = (int)p.Stock,
                Date = p.Date.ToString("dd/MM/yyyy H:mm"),
                UserName = p.User.Email,
                State = p.State
            }).ToList().OrderByDescending(d => d.Id);
        }

        public Product GetById(int id)
        {
            var entity = _aplicationDbContext.Products.FirstOrDefault(p => p.Id == id);

            return entity;
        }   
    }
}
