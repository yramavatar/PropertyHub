using AutoMapper;
using BUSINESS_LOGIC_LAYER.Services;
using DATA_ACCESS_LAYER.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PropertyHubWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                if (feedbackDTO == null)
                    return BadRequest("Feedback data is required.");

                bool result = await _feedbackService.AddFeedback(feedbackDTO);
                if (result)
                    return CreatedAtAction(nameof(GetFeedbackById), new { id = feedbackDTO.FeedbackId }, feedbackDTO);
                else
                    return StatusCode(500, "Failed to add feedback.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, FeedbackDTO feedbackDTO)
        {
            try
            {
                if (id != feedbackDTO.FeedbackId)
                    return BadRequest("Feedback ID mismatch.");

                bool result = await _feedbackService.UpdateFeedback(feedbackDTO);
                if (result)
                    return NoContent();
                else
                    return StatusCode(500, "Failed to update feedback.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            try
            {
                bool result = await _feedbackService.DeleteFeedback(id);
                if (result)
                    return NoContent();
                else
                    return StatusCode(500, "Failed to delete feedback.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            try
            {
                FeedbackDTO feedbackDTO = await _feedbackService.GetFeedbackById(id);
                if (feedbackDTO == null)
                    return NotFound($"Feedback with ID {id} not found.");

                return Ok(feedbackDTO);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet("buyer/{buyerId}")]
        //public async Task<IActionResult> GetFeedbacksByBuyerId(int buyerId)
        //{
        //    try
        //    {
        //        IEnumerable<FeedbackDTO> feedbacks = await _feedbackService.GetFeedbacksByBuyerId(buyerId);
        //        return Ok(feedbacks);
        //    }
        //    catch (ServiceException ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpGet("property/{propertyId}")]
        //public async Task<IActionResult> GetFeedbacksByPropertyId(int propertyId)
        //{
        //    try
        //    {
        //        IEnumerable<FeedbackDTO> feedbacks = await _feedbackService.GetFeedbacksByPropertyId(propertyId);
        //        return Ok(feedbacks);
        //    }
        //    catch (ServiceException ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                IEnumerable<FeedbackDTO> feedbacks = await _feedbackService.GetAllFeedbacks();
                return Ok(feedbacks);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
