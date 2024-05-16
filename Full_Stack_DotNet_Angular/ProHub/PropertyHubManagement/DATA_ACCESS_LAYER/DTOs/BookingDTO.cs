using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int BuyerId { get; set; }
        // Original  BookingStatus
        public BookingStatus Status { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
