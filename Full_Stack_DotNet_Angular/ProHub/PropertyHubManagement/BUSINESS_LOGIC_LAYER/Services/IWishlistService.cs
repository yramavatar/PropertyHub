using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
   public interface IWishlistService
    {
        Task<WishlistDTO> AddToWishlist(int propertyId, int buyerId);
        Task<bool> RemoveFromWishlist(int wishlistItemId);
      //  Task<IEnumerable<WishlistDTO>> GetWishlistByBuyerId(int buyerId);
        Task<WishlistDTO> GetWishlistItemById(int wishlistItemId);
       // Task<bool> IsPropertyInWishlist(int propertyId, int buyerId);
       // Task<bool> ClearWishlist(int buyerId);
        Task<bool> UpdateWishlistItem(WishlistDTO updatedWishlistItem);

    }
}
