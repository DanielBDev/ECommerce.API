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
    public class SaleService : ISaleService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public SaleService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public Sale Add(SaleRequest sale, string userId)
        {
            var entity = new Sale
            {
                Date = DateTime.Now,
                State = true,
                UserId = userId,
                Total = sale.Total
            };

            _aplicationDbContext.Add(entity);
            _aplicationDbContext.SaveChanges();

            foreach (var Detail in sale.DetailSales)
            {
                DetailSale detailSale = new DetailSale();
                detailSale.SaleId = entity.Id;
                detailSale.ProductId = Detail.ProductId;
                detailSale.Price = Detail.Price;
                detailSale.Quantity = Detail.Quantity;
                detailSale.Total = Detail.Quantity * Detail.Price;
                // Stock Control
                var product = _aplicationDbContext.Products.FirstOrDefault(p => p.Id == detailSale.ProductId);
                product.Stock = product.Stock - detailSale.Quantity;
                //
                _aplicationDbContext.DetailSales.Add(detailSale);
            }         

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Sale Modify(SaleRequest sale, string userId)
        {
            var entity = _aplicationDbContext.Sales.FirstOrDefault(s => s.Id == sale.Id);
            var rollback = 0;

            entity.Date = DateTime.Now;
            entity.State = true;
            entity.Total = sale.Total;
            entity.UserId = userId;

            foreach (var Detail in sale.DetailSales)
            {
                var detailSale = _aplicationDbContext.DetailSales.FirstOrDefault(ds => ds.Id == Detail.Id);
                var product = _aplicationDbContext.Products.FirstOrDefault(p => p.Id == detailSale.ProductId);
                rollback = (int)(product.Stock + detailSale.Quantity);
                detailSale.Quantity = Detail.Quantity;
                detailSale.Price = Detail.Price;

                detailSale.ProductId = Detail.ProductId;
                detailSale.SaleId = entity.Id;
                detailSale.Total = detailSale.Quantity * detailSale.Price;
                // Stock Control
                product.Stock = rollback - detailSale.Quantity;
                //
            }

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Sale Delete(int id)
        {
            var entity = _aplicationDbContext.Sales.FirstOrDefault(s => s.Id == id);
            var detailsSale = _aplicationDbContext.DetailSales.Where(s => s.SaleId == entity.Id);
            var rollback = 0;
            entity.State = false;

            foreach (var Detail in detailsSale)
            {
                var detailSale = _aplicationDbContext.DetailSales.FirstOrDefault(ds => ds.Id == Detail.Id);
                var product = _aplicationDbContext.Products.FirstOrDefault(p => p.Id == detailSale.ProductId);
                rollback = (int)(product.Stock + detailSale.Quantity);
                detailSale.Quantity = Detail.Quantity;
                detailSale.Price = Detail.Price;

                detailSale.ProductId = Detail.ProductId;
                detailSale.SaleId = entity.Id;
                detailSale.Total = detailSale.Quantity * detailSale.Price;
                // Stock Control
                product.Stock = rollback;
                //
            }
            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<SaleResponse> GetAll()
        {
            return _aplicationDbContext.Sales.Where(s => s.State == true).Select(s => new SaleResponse
            {
                Id = s.Id,
                Date = s.Date.ToString("yyyy/MM/dd H:mm"),
                UserName = s.User.Email,
                Total = s.Total
            }).ToList().OrderByDescending(d => d.Id);
        }
    }
}
