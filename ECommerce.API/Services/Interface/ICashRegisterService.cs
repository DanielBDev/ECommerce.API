using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ECommerce.API.Services.Interface
{
    public interface ICashRegisterService
    {
        CashRegister OpenCashRegister(CashRegisterRequest cashRegister, IdentityUser user);
        CashRegister CloseCashRegister(CashRegisterRequest cashRegister, IdentityUser user);
        IEnumerable<CashRegisterRequest> GetAll();
    }
}
