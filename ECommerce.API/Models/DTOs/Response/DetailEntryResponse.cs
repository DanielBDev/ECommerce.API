using ECommerce.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Models.DTOs.Response
{
    public class DetailEntryResponse
    {       

        public int Id { get; set; }
        public string DescriptionEntry { get; set; }
        public string Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public string UserName { get; set; }
        public List<DetailEntry> DetailEntries { get; set; }
    }
    public class Detail
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        //public int EntryId { get; set; }
    }
}
