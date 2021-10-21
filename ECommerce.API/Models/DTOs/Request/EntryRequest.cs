using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models.DTOs.Request
{
    public class EntryRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public bool State { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DetailEntryDto> DetailEntries { get; set; }
        public int ProviderId { get; set; }
    }
    public class DetailEntryDto
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public int ProductId { get; set; }
    }
}
