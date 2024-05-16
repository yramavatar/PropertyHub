using AutoMapper;
using DATA_ACCESS_LAYER.Data_Models;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly PropertyHubDbContext _context;
        private readonly IMapper _mapper;

        public FeedbackRepository(PropertyHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddFeedback(FeedbackDTO feedbackData)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackData);
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new FeedbackException("Failed to add feedback", ex);
            }
        }

        public async Task<bool> UpdateFeedback(FeedbackDTO feedbackData)
        {
            try
            {
                var existingFeedback = await _context.Feedbacks.FindAsync(feedbackData.FeedbackId);
                if (existingFeedback == null)
                    throw new NotFoundException("Feedback not found");

                _mapper.Map(feedbackData, existingFeedback);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new FeedbackException("Failed to update feedback", ex);
            }
        }

        public async Task<bool> DeleteFeedback(int feedbackId)
        {
            try
            {
                var feedback = await _context.Feedbacks.FindAsync(feedbackId);
                if (feedback == null)
                    throw new NotFoundException("Feedback not found");

                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new FeedbackException("Failed to delete feedback", ex);
            }
        }

        public async Task<FeedbackDTO> GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedback = await _context.Feedbacks.FindAsync(feedbackId);
                if (feedback == null)
                    throw new NotFoundException("Feedback not found");

                return _mapper.Map<FeedbackDTO>(feedback);
            }
            catch (Exception ex)
            {
                throw new FeedbackException("Failed to retrieve feedback", ex);
            }
        }

        //public async Task<IEnumerable<FeedbackDTO>> GetFeedbacksByBuyerId(int buyerId)
        //{
        //    try
        //    {
        //        var feedbacks = await _context.Feedbacks
        //            .Where(f => f.BuyerId == buyerId)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new FeedbackException("Failed to retrieve feedbacks by buyer ID", ex);
        //    }
        //}

        //public async Task<IEnumerable<FeedbackDTO>> GetFeedbacksByPropertyId(int propertyId)
        //{
        //    try
        //    {
        //        var feedbacks = await _context.Feedbacks
        //            .Where(f => f.PropertyId == propertyId)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new FeedbackException("Failed to retrieve feedbacks by property ID", ex);
        //    }
        //}

        public async Task<IEnumerable<FeedbackDTO>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _context.Feedbacks.ToListAsync();
                return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
            }
            catch (Exception ex)
            {
                throw new FeedbackException("Failed to retrieve all feedbacks", ex);
            }
        }

    }

    [Serializable]
    public class FeedbackException : Exception
    {
        public FeedbackException()
        {
        }

        public FeedbackException(string? message) : base(message)
        {
        }

        public FeedbackException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FeedbackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
