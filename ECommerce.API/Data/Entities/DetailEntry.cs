using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Data.Entities
{
    public class DetailEntry
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }        
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int EntryId { get; set; }
        public Entry Entry { get; set; }
    }
}
