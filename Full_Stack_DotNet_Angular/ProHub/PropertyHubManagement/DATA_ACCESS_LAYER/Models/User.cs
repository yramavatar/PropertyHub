using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Models
{

    public class User
    {
        [Key]
        public int userId { get; set; }  //properties or field / naming 

         
        public string username { get; set; }

       
        public string email { get; set; }



      
        public string password { get; set; }

        // Additional Properties 
       
         public string firstName { get; set; }
         
        public string lastName { get; set; }

    
        public string contactNumber { get; set; }
        public UserType userType { get; set; }




        // Navigation Properties
        public ICollection<Property> OwnedProperties { get; set; }
        public ICollection<Booking> BookedProperties { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Wishlist> WishlistItems { get; set; }

    }

    public enum UserType
    {
        [Display(Name ="Buyer")]
        Buyer,
        [Display(Name ="PropertyOwner")]
        PropertyOwner

    }

}



 



