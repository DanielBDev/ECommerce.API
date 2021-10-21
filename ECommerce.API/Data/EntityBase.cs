using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Data
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool State { get; set; }
    }
}
