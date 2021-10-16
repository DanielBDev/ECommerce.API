using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Data.Entities
{
    public class Product : EntityBase
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }
        public int? Stock { get; set; }
        public List<DetailEntry> DetailEntries { get; set; }
        public List<DetailLost> DetailLosts { get; set; }
        public List<DetailSale> DetailSales { get; set; }
        ////////////////////////
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
