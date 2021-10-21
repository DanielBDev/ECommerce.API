using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface IEntryService
    {
        Entry Add(EntryRequest entry, string userId);
        Entry Modify(EntryRequest entry, string userId);
        Entry Delete(int id);
        IEnumerable<EntryResponse> GetAll();
    }
}
