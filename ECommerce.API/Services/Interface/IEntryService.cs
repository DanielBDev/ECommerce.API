using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs.Response;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface IEntryService
    {
        Entry Add(EntryRequest entry);
        Entry Modify(EntryRequest entry);
        Entry Delete(int id);
        IEnumerable<EntryResponse> GetAll();


        //IEnumerable<DetailEntryResponse> GetEntryById(int idEntry);

        //object GetEntryId(int id);

        //IEnumerable<> GetEntryId(int idEntry);

        //IEnumerable<EntryResponse> GetByDate(DateTime date);

        //IEnumerable<EntryResponse> GetByDate(DateTime date);

        //IEnumerable<EntryViewModel> GetByCode(string code);

        //EntryViewModel GetById(int id);
    }
}
