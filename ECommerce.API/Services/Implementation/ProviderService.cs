using ECommerce.API.Data;
using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using ECommerce.API.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.API.Services.Implementation
{
    public class ProviderService : IProviderService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public ProviderService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public Provider Add(ProviderRequest provider)
        {
            var entity = new Provider
            {
                Name = provider.Name,
                Email = provider.Email,
                Cuil = provider.Cuil,
                State = true
            };

            _aplicationDbContext.Add(entity);
            _aplicationDbContext.SaveChanges();

            return entity;
        }
        public Provider Modify(ProviderRequest provider)
        {
            var entity = _aplicationDbContext.Providers.FirstOrDefault(x => x.Id == provider.Id);

            entity.Name = provider.Name;
            entity.Email = provider.Email;
            entity.Cuil = provider.Cuil;
            entity.State = true;

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public Provider Delete(int id)
        {
            var entity = _aplicationDbContext.Providers.FirstOrDefault(x => x.Id == id);

            entity.State = false;

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<ProviderResponse> GetAll()
        {
            return _aplicationDbContext.Providers.Where(p => p.State == true).Select(p => new ProviderResponse
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Cuil = p.Cuil,
                State = p.State
            }).ToList().OrderByDescending(d => d.Id);
        }

        public Provider GetById(int id)
        {
            var entity = _aplicationDbContext.Providers.FirstOrDefault(p => p.Id == id);

            return entity;
        }
    }
}
