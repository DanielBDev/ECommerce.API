using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Data.Entities
{
    public class Provider : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public int Cuil { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
