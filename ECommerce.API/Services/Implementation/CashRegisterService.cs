using ECommerce.API.Data;
using ECommerce.API.Data.Entities;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Services.Implementation
{
    public class CashRegisterService : ICashRegisterService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public CashRegisterService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public CashRegister OpenCashRegister(CashRegisterRequest cashRegister, IdentityUser user)
        {
            var entity = new CashRegister
            {
                OpeningAmount = cashRegister.OpeningAmount,
                OpeningDate = DateTime.Now,
                AmountSystem = 0,
                OpeningUserName = user.UserName,
                State = true,
            };

            _aplicationDbContext.Add(entity);
            _aplicationDbContext.SaveChanges();

            foreach (var Detail in cashRegister.DetailCashRegisters)
            {
                DetailCashRegister detailCashRegister = new DetailCashRegister();
                detailCashRegister.Amount = Detail.Amount;
                detailCashRegister.CashRegisterId = entity.Id;

                _aplicationDbContext.DetailCashRegisters.Add(detailCashRegister);
            }

            _aplicationDbContext.SaveChanges();

            return entity;
        }

        public CashRegister CloseCashRegister(CashRegisterRequest cashRegister, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashRegisterRequest> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
