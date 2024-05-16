using AutoMapper;
using DATA_ACCESS_LAYER.Data_Models;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using Microsoft.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly PropertyHubDbContext _context;
        private readonly IMapper _mapper;


        public BookingRepository(PropertyHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
       


        public async Task<BookingDTO> BookProperty(BookingDTO bookingDTO)
        {
            try
            {
                var booking = _mapper.Map<Booking>(bookingDTO);
                booking.BookingDate = DateTime.Now;

                // Check if the property exists
                var property = await _context.Properties.FindAsync(booking.PropertyId);
                if (property == null)
                {
                    throw new Exception("Property not found.");
                }

                // Check if the property is available
                if (property.Status == PropertyStatus.Available)
                {
                    // Update property status based on booking status
                    PropertyStatus newPropertyStatus;
                    switch (bookingDTO.Status)
                    {
                        case BookingStatus.Confirmed:
                            newPropertyStatus = PropertyStatus.Booked;
                            break;
                        case BookingStatus.Cancelled:
                            newPropertyStatus = PropertyStatus.Sold;
                            break;
                        case BookingStatus.Pending:
                            newPropertyStatus = PropertyStatus.Rented;
                            break;
                        default:
                            throw new ArgumentException("Invalid booking status.");
                    }

                    // Update property status
                    property.Status = newPropertyStatus;

                    // Save changes to property
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Property not available, return default message
                    throw new Exception("Property is not available for booking.");
                }

                // Add booking to context and save changes
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return _mapper.Map<BookingDTO>(booking);
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to book the property", ex);
            }
        }

       

        public async Task<bool> CancelBooking(int bookingId)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(bookingId);
                if (booking == null)
                    throw new NotFoundException("Booking not found");

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to cancel booking", ex);
            }
        }

        public async Task<BookingDTO> GetBookingById(int bookingId)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(bookingId);
                if (booking == null)
                    throw new NotFoundException("Booking not found");

                return _mapper.Map<BookingDTO>(booking);
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to retrieve booking", ex);
            }
        }

       

        public async Task<IEnumerable<BookingDTO>> GetBookingsByBuyerId(int buyerId)
        {
            try
            {
                var bookings = await _context.Bookings
                    .Where(b => b.BuyerId == buyerId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to retrieve bookings by buyer ID", ex);
            }
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByPropertyId(int propertyId)
        {
            try
            {
                var bookings = await _context.Bookings
                    .Where(b => b.PropertyId == propertyId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to retrieve bookings by property ID", ex);
            }
        }

        //public async Task<IEnumerable<BookingDTO>> GetBookingsByStatus(BookingStatus status)
        //{
        //    try
        //    {
        //        var bookings = await _context.Bookings
        //            .Where(b => b.Status == status)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BookingException("Failed to retrieve bookings by status", ex);
        //    }
        //}

        //public async Task<IEnumerable<BookingDTO>> SearchBookingsByStatus(BookingStatus status)
        //{
        //    try
        //    {
        //        var bookings = await _context.Bookings
        //            .Where(b => b.Status == status)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BookingException("Failed to search bookings by status", ex);
        //    }
        //}

        public async Task<IEnumerable<BookingDTO>> GetAllBookings()
        {
            try
            {
                var bookings = await _context.Bookings.ToListAsync();
                return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
            }
            catch (Exception ex)
            {
                throw new BookingException("Failed to retrieve all bookings", ex);
            }
        }

    }

    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class BookingException : Exception
    {
        public BookingException()
        {
        }

        public BookingException(string? message) : base(message)
        {
        }

        public BookingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BookingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}







//public async Task<BookingDTO> BookProperty(BookingDTO bookingDTO)
//{
//    try
//    {
//        var booking = _mapper.Map<Booking>(bookingDTO);

//        // Set booking date to current date
//        booking.BookingDate = DateTime.Now;

//        // Check if the property exists
//        var property = await _context.Properties.FindAsync(booking.PropertyId);
//        if (property == null)
//        {
//            throw new Exception("Property not found.");
//        }

//        // Check if the property is available
//        if (property.Status == PropertyStatus.Available)
//        {
//            // Check the provided status
//            switch (bookingDTO.Status)
//            {
//                case BookingStatus.Confirmed:
//                    property.Status = PropertyStatus.Booked;
//                    break;
//                case BookingStatus.Cancelled:
//                    property.Status = PropertyStatus.Sold;
//                    break;
//                case BookingStatus.Pending:
//                    property.Status = PropertyStatus.Rented;
//                    break;
//                default:
//                    throw new ArgumentException("Invalid booking status.");
//            }

//            // Save changes to property
//            await _context.SaveChangesAsync();

//            // Return the booking DTO with updated status
//            return new BookingDTO
//            {
//                BookingId = booking.BookingId,
//                PropertyId = booking.PropertyId,
//                BuyerId = booking.BuyerId,
//                Status = bookingDTO.Status,
//               // Status = (BookingStatus)PropertyStatus.Booked,
//                BookingDate = booking.BookingDate
//            };
//        }
//        else  
//        {
//            // Property not available, return default message
//            return new BookingDTO
//            {
//                BookingId = booking.BookingId,
//                PropertyId = booking.PropertyId,
//                BuyerId = booking.BuyerId,
//                Status = BookingStatus.Pending,
//                // Status = (BookingStatus)PropertyStatus.Rented ,
//                BookingDate = booking.BookingDate
//            };
//        }
//    }
//    catch (Exception ex)
//    {
//        throw new BookingException("Failed to book the property", ex);
//    }
//}

