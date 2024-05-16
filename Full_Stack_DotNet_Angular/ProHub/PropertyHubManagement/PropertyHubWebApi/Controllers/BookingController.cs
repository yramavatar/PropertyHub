using AutoMapper;
using BUSINESS_LOGIC_LAYER.Services;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PropertyHubWebApi.Controllers
{


    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookProperty([FromBody] BookingDTO bookingDTO)
        {
            try
            {
                var result = await _bookingService.BookProperty(bookingDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                var success = await _bookingService.CancelBooking(bookingId);
                if (!success)
                    return NotFound($"Booking with ID {bookingId} not found");
                return Ok($"Booking with ID {bookingId} cancelled successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            try
            {
                var booking = await _bookingService.GetBookingById(bookingId);
                if (booking == null)
                    return NotFound($"Booking with ID {bookingId} not found");
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("byBuyer/{buyerId}")]
        public async Task<IActionResult> GetBookingsByBuyerId(int buyerId)
        {
            try
            {
                var bookings = await _bookingService.GetBookingsByBuyerId(buyerId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("byProperty/{propertyId}")]
        public async Task<IActionResult> GetBookingsByPropertyId(int propertyId)
        {
            try
            {
                var bookings = await _bookingService.GetBookingsByPropertyId(propertyId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet("byStatus/{status}")]
        //public async Task<IActionResult> GetBookingsByStatus(string status)
        //{
        //    try
        //    {
        //        var bookingStatus = Enum.Parse<BookingStatus>(status, true);
        //        var bookings = await _bookingService.GetBookingsByStatus(bookingStatus);
        //        return Ok(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpGet("searchByStatus/{status}")]
        //public async Task<IActionResult> SearchBookingsByStatus(string status)
        //{
        //    try
        //    {
        //        var bookingStatus = Enum.Parse<BookingStatus>(status, true);
        //        var bookings = await _bookingService.SearchBookingsByStatus(bookingStatus);
        //        return Ok(bookings);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpGet("allBooking")]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookings();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
