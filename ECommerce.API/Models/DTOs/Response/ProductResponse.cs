using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Models.DTOs.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int Stock { get; set; }
        public bool State { get; set; }
        public string UserName { get; set; }
    }
}
