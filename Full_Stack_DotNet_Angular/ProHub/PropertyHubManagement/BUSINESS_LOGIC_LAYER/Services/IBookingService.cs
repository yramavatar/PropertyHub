using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public interface IBookingService
    {
        Task<BookingDTO> BookProperty(BookingDTO bookingDTO);
        Task<bool> CancelBooking(int bookingId);
        Task<BookingDTO> GetBookingById(int bookingId);
        Task<IEnumerable<BookingDTO>> GetBookingsByBuyerId(int buyerId);
        Task<IEnumerable<BookingDTO>> GetBookingsByPropertyId(int propertyId);
        //Task<IEnumerable<BookingDTO>> GetBookingsByStatus(BookingStatus status);
        //Task<IEnumerable<BookingDTO>> SearchBookingsByStatus(BookingStatus status);
        Task<IEnumerable<BookingDTO>> GetAllBookings();
    }
}
