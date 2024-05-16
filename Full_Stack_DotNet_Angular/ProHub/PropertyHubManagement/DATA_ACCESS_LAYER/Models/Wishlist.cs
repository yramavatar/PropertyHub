using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Models
{
     
    public class Wishlist
    {
        [Key]
        public int WishlistItemId { get; set; }

   
        public int BuyerId { get; set; }

        public User Buyer { get; set; }


        
        public int PropertyId { get; set; }

        public Property Property { get; set; }


        
    }
}
