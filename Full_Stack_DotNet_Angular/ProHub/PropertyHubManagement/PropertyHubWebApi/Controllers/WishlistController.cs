using AutoMapper;
using BUSINESS_LOGIC_LAYER.Services;
using DATA_ACCESS_LAYER.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PropertyHubWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly IMapper _mapper;

        public WishlistController(IWishlistService wishlistService, IMapper mapper)
        {
            _wishlistService = wishlistService;
            _mapper = mapper;
        }

        [HttpPost("{propertyId}/{buyerId}")]
        public async Task<ActionResult<WishlistDTO>> AddToWishlist(int propertyId, int buyerId)
        {
            try
            {
                var wishlistItem = await _wishlistService.AddToWishlist(propertyId, buyerId);
                return Ok(_mapper.Map<WishlistDTO>(wishlistItem));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{wishlistItemId}")]
        public async Task<ActionResult<bool>> RemoveFromWishlist(int wishlistItemId)
        {
            try
            {
                var result = await _wishlistService.RemoveFromWishlist(wishlistItemId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet("buyer/{buyerId}")]
        //public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetWishlistByBuyerId(int buyerId)
        //{
        //    try
        //    {
        //        var wishlistItems = await _wishlistService.GetWishlistByBuyerId(buyerId);
        //        return Ok(_mapper.Map<IEnumerable<WishlistDTO>>(wishlistItems));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpGet("{wishlistItemId}")]
        public async Task<ActionResult<WishlistDTO>> GetWishlistItemById(int wishlistItemId)
        {
            try
            {
                var wishlistItem = await _wishlistService.GetWishlistItemById(wishlistItemId);
                if (wishlistItem == null)
                    return NotFound($"Wishlist item with ID {wishlistItemId} not found.");
                return Ok(_mapper.Map<WishlistDTO>(wishlistItem));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet("check/{propertyId}/{buyerId}")]
        //public async Task<ActionResult<bool>> IsPropertyInWishlist(int propertyId, int buyerId)
        //{
        //    try
        //    {
        //        var result = await _wishlistService.IsPropertyInWishlist(propertyId, buyerId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpDelete("clear/{buyerId}")]
        //public async Task<ActionResult<bool>> ClearWishlist(int buyerId)
        //{
        //    try
        //    {
        //        var result = await _wishlistService.ClearWishlist(buyerId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateWishlistItem(WishlistDTO updatedWishlistItem)
        {
            try
            {
                var result = await _wishlistService.UpdateWishlistItem(_mapper.Map<WishlistDTO>(updatedWishlistItem));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
