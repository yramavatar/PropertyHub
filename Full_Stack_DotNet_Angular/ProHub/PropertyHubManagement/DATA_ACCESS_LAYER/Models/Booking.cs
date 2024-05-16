using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATA_ACCESS_LAYER.Models
{
     
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

      
        public int PropertyId { get; set; }

        public Property Property { get; set; }

    
        public int BuyerId { get; set; }

        public User Buyer { get; set; }

     
        public BookingStatus Status { get; set; }

       
        public DateTime BookingDate { get; set; }
    }

    public enum BookingStatus
    {
        [Display(Name = "Confirmed")]
        Confirmed,
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Pending")]
        Pending
    }

}
