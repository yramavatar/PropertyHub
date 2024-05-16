using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public interface IFeedbackService
    {
        Task<bool> AddFeedback(FeedbackDTO feedbackData);
        Task<bool> UpdateFeedback(FeedbackDTO feedbackData);
        Task<bool> DeleteFeedback(int feedbackId);
        Task<FeedbackDTO> GetFeedbackById(int feedbackId);
        //Task<IEnumerable<FeedbackDTO>> GetFeedbacksByBuyerId(int buyerId);
        //Task<IEnumerable<FeedbackDTO>> GetFeedbacksByPropertyId(int propertyId);
        Task<IEnumerable<FeedbackDTO>> GetAllFeedbacks();
    }
}
