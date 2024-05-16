using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.DTOs
{
    public class WishlistDTO
    {
        public int WishlistItemId { get; set; }
        public int BuyerId { get; set; }
        public int PropertyId { get; set; }

        
    }
}
