using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Data.Entities
{
    public class DetailLost
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }
        public int LostId { get; set; }
        public Lost Lost { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
