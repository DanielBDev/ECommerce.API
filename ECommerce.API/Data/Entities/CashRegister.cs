using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Data.Entities
{
    public class CashRegister
    {
        public int Id { get; set; }
        public bool State { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosingAmount { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountSystem { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Diference { get; set; }
        //public IdentityUser OpeningUserId { get; set; }        
        public string OpeningUserName { get; set; }
        public string ClosingUserName { get; set; }
        //public IdentityUser ClosingUser { get; set; }
        public List<DetailCashRegister> DetailCashRegisters { get; set; }
        //public List<Sale> Sales { get; set; }
    }
}
