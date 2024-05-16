using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Models
{
   
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public User Owner { get; set; }


        public PropertyType? Type { get; set; }
        // public string? Type { get; set; }

        public FlatType? FlatType { get; set; }

        public int? SizeSqFt { get; set; }

 
        public string Description { get; set; }
 
        public decimal Price { get; set; }

         
        public string Location { get; set; }

     
        public string City { get; set; }

        public PropertyStatus Status { get; set; }


        
        //public decimal? MinPrice { get; set; }
       
        //public decimal? MaxPrice { get; set; }

      
        public string ImageUrl { get; set; }

        // Navigation Properties
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Wishlist> WishlistedBy { get; set; }
    }

    public enum PropertyType
    {
        [Display(Name = "Apartment")]
        Apartment,
        [Display(Name = "Commercial")]
        Commercial,
        [Display(Name = "Villa")]
        Villa
    }

    public enum FlatType
    {
        [Display(Name = "1 BHK")]
        _1BHK,
        [Display(Name = "2 BHK")]
        _2BHK,
        [Display(Name = "3 BHK")]
        _3BHK,
        [Display(Name = "4 BHK")]
        _4BHK
    }

    public enum PropertyStatus
    {
        [Display(Name = "Avialable")]
        Available,
        [Display(Name = "Rented")]
        Rented,
        [Display(Name = "Sold")]
        Sold,
        [Display(Name = "Booked")]
        Booked
    }

}




