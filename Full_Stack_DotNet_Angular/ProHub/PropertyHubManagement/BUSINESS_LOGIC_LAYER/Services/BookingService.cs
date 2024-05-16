using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper,IPropertyRepository propertyRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

       



        public async Task<BookingDTO> BookProperty(BookingDTO bookingDTO)
        {
            try
            {
                var booking = _mapper.Map<Booking>(bookingDTO);
                var result = await _bookingRepository.BookProperty(bookingDTO);
                return _mapper.Map<BookingDTO>(result);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while booking property", ex);
            }
        }





        public async Task<bool> CancelBooking(int bookingId)
        {
            try
            {
                return await _bookingRepository.CancelBooking(bookingId);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while cancelling booking", ex);
            }
        }

        public async Task<BookingDTO> GetBookingById(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingById(bookingId);
                return _mapper.Map<BookingDTO>(booking);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while getting booking by ID", ex);
            }
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByBuyerId(int buyerId)
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsByBuyerId(buyerId);
                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while getting bookings by buyer ID", ex);
            }
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByPropertyId(int propertyId)
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsByPropertyId(propertyId);
                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while getting bookings by property ID", ex);
            }
        }

        //public async Task<IEnumerable<BookingDTO>> GetBookingsByStatus(BookingStatus status)
        //{
        //    try
        //    {
        //        var bookings = await _bookingRepository.GetBookingsByStatus(status);
        //        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AppException("Error occurred while getting bookings by status", ex);
        //    }
        //}

        //public async Task<IEnumerable<BookingDTO>> SearchBookingsByStatus(BookingStatus status)
        //{
        //    try
        //    {
        //        var bookings = await _bookingRepository.SearchBookingsByStatus(status);
        //        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AppException("Error occurred while searching bookings by status", ex);
        //    }
        //}

        public async Task<IEnumerable<BookingDTO>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllBookings();
                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new AppException("Error occurred while getting all bookings", ex);
            }
        }
    }

    [Serializable]
    public class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string? message) : base(message)
        {
        }

        public AppException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}










// Newly added proeprty
//public async Task<BookingDTO> BookProperty(BookingDTO bookingDTO)
//{
//    try
//    {
//        var booking = _mapper.Map<Booking>(bookingDTO);

//        // Set booking date to current date
//        booking.BookingDate = DateTime.Now;

//        // Check if the property exists
//        var property = await _propertyRepository.GetPropertyById(booking.PropertyId);
//        if (property == null)
//        {
//            throw new NotFoundException("Property not found.");
//        }

//        // Check if the property is available
//        if (property.PropertyStatus == "Available" || property.PropertyStatus == "Booked")
//        {
//            // Check the provided status
//            if (bookingDTO.Status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
//            {
//                // Update property status to "Booked"
//                property.PropertyStatus = "Booked";

//                // Save changes to property
//                await _propertyRepository.UpdateProperty(property);

//                // Construct the success message
//                string message = $"Property with ID {property.PropertyId} has been booked.";

//                // Return the success message along with the booking DTO
//                return new BookingDTO
//                {
//                    BookingId = booking.BookingId,
//                    PropertyId = booking.PropertyId,
//                    BuyerId = booking.BuyerId,
//                    Status = message,
//                    BookingDate = booking.BookingDate
//                };
//            }
//            else if (bookingDTO.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) ||
//                     bookingDTO.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
//            {
//                // Return default message if status is "Cancelled" or "Pending"
//                return new BookingDTO
//                {
//                    BookingId = booking.BookingId,
//                    PropertyId = booking.PropertyId,
//                    BuyerId = booking.BuyerId,
//                    Status = "Booking is cancelled or pending.",
//                    BookingDate = booking.BookingDate
//                };
//            }
//            else
//            {
//                // Return default message if status is not recognized
//                return new BookingDTO
//                {
//                    BookingId = booking.BookingId,
//                    PropertyId = booking.PropertyId,
//                    BuyerId = booking.BuyerId,
//                    Status = "Booking status is not recognized.",
//                    BookingDate = booking.BookingDate
//                };
//            }
//        }
//        else
//        {
//            // Property not available, return default message
//            return new BookingDTO
//            {
//                BookingId = booking.BookingId,
//                PropertyId = booking.PropertyId,
//                BuyerId = booking.BuyerId,
//                Status = "Property is not available for booking.",
//                BookingDate = booking.BookingDate
//            };
//        }
//    }
//    catch (Exception ex)
//    {
//        throw new BookingException("Failed to book the property", ex);
//    }
//}