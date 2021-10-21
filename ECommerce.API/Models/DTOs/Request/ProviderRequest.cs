using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Models.DTOs.Request
{
    public class ProviderRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Cuil { get; set; }
        public bool State { get; set; }
    }
}
