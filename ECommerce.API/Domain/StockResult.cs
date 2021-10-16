using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Domain
{
    public class StockResult
    {
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
