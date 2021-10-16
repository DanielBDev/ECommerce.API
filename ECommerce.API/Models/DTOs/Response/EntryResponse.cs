using ECommerce.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models.DTOs.Response
{
    public class EntryResponse
    {
        public int Id { get; set; }
        public string DescriptionEntry { get; set; }
        public string Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public string UserName { get; set; }
    }
}
