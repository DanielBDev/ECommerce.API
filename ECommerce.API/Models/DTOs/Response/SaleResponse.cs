using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models.DTOs.Response
{
    public class SaleResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public string UserName { get; set; }
    }
}
