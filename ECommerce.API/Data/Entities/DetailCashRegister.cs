using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Data.Entities
{
    public class DetailCashRegister
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int CashRegisterId { get; set; }
        public CashRegister CashRegister { get; set; }
    }
}
