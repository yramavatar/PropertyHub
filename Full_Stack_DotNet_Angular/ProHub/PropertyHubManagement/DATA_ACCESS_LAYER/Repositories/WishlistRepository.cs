using AutoMapper;
using DATA_ACCESS_LAYER.Data_Models;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly PropertyHubDbContext _context;
        private readonly IMapper _mapper;

        public WishlistRepository(PropertyHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WishlistDTO> AddToWishlist(int propertyId, int buyerId)
        {
            var wishlistItem = new Wishlist { PropertyId = propertyId, BuyerId = buyerId };
            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<WishlistDTO>(wishlistItem);
        }

        public async Task<bool> RemoveFromWishlist(int wishlistItemId)
        {
            var wishlistItem = await _context.Wishlists.FindAsync(wishlistItemId);
            if (wishlistItem == null)
                return false;

            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<IEnumerable<WishlistDTO>> GetWishlistByBuyerId(int buyerId)
        //{
        //    var wishlistItems = await _context.Wishlists
        //        .Where(w => w.BuyerId == buyerId)
        //        .ToListAsync();
        //    return _mapper.Map<IEnumerable<WishlistDTO>>(wishlistItems);
        //}

        public async Task<WishlistDTO> GetWishlistItemById(int wishlistItemId)
        {
            var wishlistItem = await _context.Wishlists.FindAsync(wishlistItemId);
            return _mapper.Map<WishlistDTO>(wishlistItem);
        }

        //public async Task<bool> IsPropertyInWishlist(int propertyId, int buyerId)
        //{
        //    return await _context.Wishlists
        //        .AnyAsync(w => w.PropertyId == propertyId && w.BuyerId == buyerId);
        //}

        //public async Task<bool> ClearWishlist(int buyerId)
        //{
        //    var wishlistItems = await _context.Wishlists
        //        .Where(w => w.BuyerId == buyerId)
        //        .ToListAsync();

        //    if (wishlistItems == null || !wishlistItems.Any())
        //        return false;

        //    _context.Wishlists.RemoveRange(wishlistItems);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> UpdateWishlistItem(WishlistDTO updatedWishlistItem)
        {
            var wishlistItem = await _context.Wishlists.FindAsync(updatedWishlistItem.WishlistItemId);
            if (wishlistItem == null)
                return false;

            wishlistItem.PropertyId = updatedWishlistItem.PropertyId;
            wishlistItem.BuyerId = updatedWishlistItem.BuyerId;

            _context.Wishlists.Update(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
