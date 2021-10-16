using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Data
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool State { get; set; }
        //[Required]
        //public string UserName { get; set; }
    }
}
