using AutoMapper;
using BUSINESS_LOGIC_LAYER.Services;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PropertyHubWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty([FromBody] PropertyDTO propertyData)
        {
            try
            {
                await _propertyService.AddProperty(propertyData);
                return Ok("Property Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetPropertyById(int propertyId)
        {
            try
            {
                var property = await _propertyService.GetPropertyById(propertyId);
                return Ok(property);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            try
            {
                var properties = await _propertyService.GetAllProperties();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyData)
        {
            try
            {
                var result = await _propertyService.UpdateProperty(propertyData);
                if (result)
                    return Ok();
                else
                    return BadRequest("Failed to update property.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{propertyId}/status")]
        public async Task<IActionResult> GetPropertyStatus(int propertyId)
        {
            try
            {
                var status = await _propertyService.GetPropertyStatus(propertyId);
                return Ok(status);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet("flat-types")]
        //public async Task<IActionResult> GetFlatTypes()
        //{
        //    try
        //    {
        //        var flatTypes = await _propertyService.GetFlatTypes();
        //        return Ok(flatTypes);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            try
            {
                var result = await _propertyService.DeleteProperty(propertyId);
                if (result)
                    return Ok();
                else
                    return BadRequest("Failed to delete property.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search/city/{city}")]
        public async Task<IActionResult> SearchPropertiesByCity(string city)
        {
            try
            {
                var properties = await _propertyService.SearchPropertiesByCity(city);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet("search/price")]
        //public async Task<IActionResult> SearchPropertiesByPriceRange(decimal? minPrice, decimal? maxPrice)
        //{
        //    try
        //    {
        //        var properties = await _propertyService.SearchPropertiesByPrice(minPrice, maxPrice);
        //        return Ok(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet("search/type/{type}")]
        //public async Task<IActionResult> SearchPropertiesByType(PropertyType type)
        //{
        //    try
        //    {
        //        var properties = await _propertyService.SearchPropertiesByType(type);
        //        return Ok(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet("search/flat-type/{flatType}")]
        //public async Task<IActionResult> SearchPropertiesByFlatType(FlatType flatType)
        //{
        //    try
        //    {
        //        var properties = await _propertyService.SearchPropertiesByFlat(flatType);
        //        return Ok(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
