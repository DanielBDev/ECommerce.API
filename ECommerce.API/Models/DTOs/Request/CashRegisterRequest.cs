using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Models.DTOs.Request
{
    public class CashRegisterRequest
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosingAmount { get; set; }
        public DateTime OpeningDate { get; set; }
        public System.DateTime ClosingDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountSystem { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Diference { get; set; }
        public IdentityUser OpeningUserId { get; set; }
        public IdentityUser ClosingUserId { get; set; }
        public string OpeningUserName { get; set; }
        public string ClosingUserName { get; set; }
        public List<DetailCashRegisterDto> DetailCashRegisters { get; set; }
    }

    public class DetailCashRegisterDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int CashRegisterId { get; set; }
    }
}
