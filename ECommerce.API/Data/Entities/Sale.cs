using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Data.Entities
{
    public class Sale : EntityBase
    {
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public List<DetailSale> DetailSales { get; set; }
        //public int CashRegisterId { get; set; }
        //public CashRegister CashRegister { get; set; }
        ////////////////////////
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
