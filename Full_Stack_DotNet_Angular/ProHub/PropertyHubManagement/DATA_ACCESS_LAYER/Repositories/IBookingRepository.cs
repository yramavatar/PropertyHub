using DATA_ACCESS_LAYER.DTOs;
using System.Net.NetworkInformation;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DATA_ACCESS_LAYER.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingDTO> BookProperty(BookingDTO bookingDTO);
        Task<bool> CancelBooking(int bookingId);
        Task<BookingDTO> GetBookingById(int bookingId);
        Task<IEnumerable<BookingDTO>> GetBookingsByBuyerId(int buyerId);
        Task<IEnumerable<BookingDTO>> GetBookingsByPropertyId(int propertyId);
       
        Task<IEnumerable<BookingDTO>> GetAllBookings();
    }
}






//Task<IEnumerable<BookingDTO>> GetBookingsByStatus(BookingStatus status);
//Task<IEnumerable<BookingDTO>> SearchBookingsByStatus(BookingStatus status);