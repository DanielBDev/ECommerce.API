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
    public class EntryService : IEntryService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public EntryService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public Entry Add(EntryRequest entry, string userId)
        {
            var entity = new Entry
            {
                Description = entry.Description,
                Date = DateTime.Now,
                State = true,
                UserId = userId,
                Total = entry.Total,
                ProviderId = entry.ProviderId
            };

            _aplicationDbContext.Add(entity);
            _aplicationDbContext.SaveChanges();

            foreach (var Detail in entry.DetailEntries)
            {
                DetailEntry detailEntry = new DetailEntry();
                detailEntry.EntryId = entity.Id;
                detailEntry.ProductId = Detail.ProductId;
                detailEntry.Cost = Detail.Cost;
                detailEntry.Quantity = Detail.Quantity;
                detailEntry.Total = Detail.Quantity * Detail.Cost;
                // Stock & Cost Control
                var product = _aplicationDbContext.Products.FirstOrDefault(x => x.Id == detailEntry.ProductId);
                product.Stock = product.Stock + detailEntry.Quantity;
                product.Cost = detailEntry.Cost;
                //
                _aplicationDbContext.DetailEntries.Add(detailEntry);
            }

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Entry Modify(EntryRequest entry, string userId)
        {
            var entity = _aplicationDbContext.Entries.FirstOrDefault(x => x.Id == entry.Id);
            var rollback = 0;

            entity.Description = entry.Description;
            entity.Date = DateTime.Now;
            entity.Total = entry.Total;
            entity.State = true;
            entity.ProviderId = entry.ProviderId;
            entity.UserId = userId;

            foreach (var Detail in entry.DetailEntries)
            {
                var detailEntry = _aplicationDbContext.DetailEntries.FirstOrDefault(de => de.Id == Detail.Id);
                var product = _aplicationDbContext.Products.FirstOrDefault(x => x.Id == detailEntry.ProductId);
                rollback = (int)(product.Stock - detailEntry.Quantity);
                detailEntry.Quantity = Detail.Quantity;
                detailEntry.Cost = Detail.Cost;
             
                detailEntry.ProductId = Detail.ProductId;
                detailEntry.EntryId = entity.Id;
                detailEntry.Total = Detail.Quantity * Detail.Cost;
                // Stock & Cost Control
                product.Stock = rollback + detailEntry.Quantity;
                product.Cost = detailEntry.Cost;
                //
            }
            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Entry Delete(int id)
        {
            var entity = _aplicationDbContext.Entries.FirstOrDefault(x => x.Id == id);

            var detailsEntry = _aplicationDbContext.DetailEntries.Where(de => de.EntryId == entity.Id);
            var rollback = 0;
            entity.State = false;

            foreach (var Detail in detailsEntry)
            {
                var detailEntry = _aplicationDbContext.DetailEntries.FirstOrDefault(de => de.Id == Detail.Id);
                var product = _aplicationDbContext.Products.FirstOrDefault(x => x.Id == detailEntry.ProductId);
                rollback = (int)(product.Stock - detailEntry.Quantity);
                detailEntry.Quantity = Detail.Quantity;
                detailEntry.Cost = Detail.Cost;

                detailEntry.ProductId = Detail.ProductId;
                detailEntry.EntryId = entity.Id;
                detailEntry.Total = Detail.Quantity * Detail.Cost;
                // Stock & Cost Control
                product.Stock = rollback;
                product.Cost = detailEntry.Cost;
                //
            }

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<EntryResponse> GetAll()
        {
            return _aplicationDbContext.Entries.Where(e => e.State == true).Select(e => new EntryResponse
            {
                Id = e.Id,
                DescriptionEntry = e.Description,
                Date = e.Date.ToString("yyyy/MM/dd H:mm"),
                UserName = e.User.Email,
                Total = e.Total,
                ProviderName = e.Provider.Name
            }).ToList().OrderByDescending(d => d.Id);
        }
    }
}
