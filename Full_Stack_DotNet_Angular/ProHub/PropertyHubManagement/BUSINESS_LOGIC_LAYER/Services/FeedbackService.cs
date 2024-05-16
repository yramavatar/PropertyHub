using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddFeedback(FeedbackDTO feedbackData)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackData);
                await _feedbackRepository.AddFeedback(feedbackData);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to add feedback.", ex);
            }
        }

        public async Task<bool> UpdateFeedback(FeedbackDTO feedbackData)
        {
            try
            {
                var existingFeedback = await _feedbackRepository.GetFeedbackById(feedbackData.FeedbackId);
                if (existingFeedback == null)
                    throw new NotFoundException($"Feedback with ID {feedbackData.FeedbackId} not found.");

                var feedbackToUpdate = _mapper.Map<Feedback>(feedbackData);
                await _feedbackRepository.UpdateFeedback(feedbackData);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to update feedback.", ex);
            }
        }

        public async Task<bool> DeleteFeedback(int feedbackId)
        {
            try
            {
                var existingFeedback = await _feedbackRepository.GetFeedbackById(feedbackId);
                if (existingFeedback == null)
                    throw new NotFoundException($"Feedback with ID {feedbackId} not found.");

                await _feedbackRepository.DeleteFeedback(feedbackId);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to delete feedback.", ex);
            }
        }

        public async Task<FeedbackDTO> GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedback = await _feedbackRepository.GetFeedbackById(feedbackId);
                if (feedback == null)
                    throw new NotFoundException($"Feedback with ID {feedbackId} not found.");

                return _mapper.Map<FeedbackDTO>(feedback);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get feedback by ID.", ex);
            }
        }

        //public async Task<IEnumerable<FeedbackDTO>> GetFeedbacksByBuyerId(int buyerId)
        //{
        //    try
        //    {
        //        var feedbacks = await _feedbackRepository.GetFeedbacksByBuyerId(buyerId);
        //        return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceException("Failed to get feedbacks by buyer ID.", ex);
        //    }
        //}

        //public async Task<IEnumerable<FeedbackDTO>> GetFeedbacksByPropertyId(int propertyId)
        //{
        //    try
        //    {
        //        var feedbacks = await _feedbackRepository.GetFeedbacksByPropertyId(propertyId);
        //        return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceException("Failed to get feedbacks by property ID.", ex);
        //    }
        //}

        public async Task<IEnumerable<FeedbackDTO>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetAllFeedbacks();
                return _mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get all feedbacks.", ex);
            }
        }
    }

    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string? message) : base(message)
        {
        }

        public ServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
