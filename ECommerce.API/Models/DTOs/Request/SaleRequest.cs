using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models.DTOs.Request
{
    public class SaleRequest
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public bool State { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DetailSaleDto> DetailSales { get; set; }
        public string UserId { get; set; }
    }
    public class DetailSaleDto
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public int ProductId { get; set; }
    }
}
