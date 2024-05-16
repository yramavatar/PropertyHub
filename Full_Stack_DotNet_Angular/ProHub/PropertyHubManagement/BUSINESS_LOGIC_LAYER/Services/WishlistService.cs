using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Repositories;
using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task<WishlistDTO> AddToWishlist(int propertyId, int buyerId)
        {
            try
            {
                return await _wishlistRepository.AddToWishlist(propertyId, buyerId);
            }
            catch (Exception ex)
            {
                throw new WishlistOperationException("Failed to add property to wishlist.", ex);
            }
        }

        public async Task<bool> RemoveFromWishlist(int wishlistItemId)
        {
            try
            {
                return await _wishlistRepository.RemoveFromWishlist(wishlistItemId);
            }
            catch (Exception ex)
            {
                throw new WishlistOperationException("Failed to remove property from wishlist.", ex);
            }
        }

        //public async Task<IEnumerable<WishlistDTO>> GetWishlistByBuyerId(int buyerId)
        //{
        //    try
        //    {
        //        return await _wishlistRepository.GetWishlistByBuyerId(buyerId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WishlistOperationException("Failed to retrieve wishlist items.", ex);
        //    }
        //}

        public async Task<WishlistDTO> GetWishlistItemById(int wishlistItemId)
        {
            try
            {
                return await _wishlistRepository.GetWishlistItemById(wishlistItemId);
            }
            catch (Exception ex)
            {
                throw new WishlistOperationException("Failed to retrieve wishlist item.", ex);
            }
        }

        //public async Task<bool> IsPropertyInWishlist(int propertyId, int buyerId)
        //{
        //    try
        //    {
        //        return await _wishlistRepository.IsPropertyInWishlist(propertyId, buyerId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WishlistOperationException("Failed to check if property is in wishlist.", ex);
        //    }
        //}

        //public async Task<bool> ClearWishlist(int buyerId)
        //{
        //    try
        //    {
        //        return await _wishlistRepository.ClearWishlist(buyerId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new WishlistOperationException("Failed to clear wishlist.", ex);
        //    }
        //}

        public async Task<bool> UpdateWishlistItem(WishlistDTO updatedWishlistItem)
        {
            try
            {
                return await _wishlistRepository.UpdateWishlistItem(updatedWishlistItem);
            }
            catch (Exception ex)
            {
                throw new WishlistOperationException("Failed to update wishlist item.", ex);
            }
        }
    }

    [Serializable]
   public class WishlistOperationException : Exception
    {
        public WishlistOperationException()
        {
        }

        public WishlistOperationException(string? message) : base(message)
        {
        }

        public WishlistOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WishlistOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
