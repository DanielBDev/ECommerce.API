using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models.DTOs.Request
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int Stock { get; set; }
        public bool State { get; set; }
        public string UserId { get; set; }
    }
}
