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

namespace BUSINESS_LOGIC_LAYER.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly PropertyHubDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(PropertyHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        //older one
        //public async Task AddProperty(PropertyDTO propertyData)
        //{
        //    try
        //    {
        //        var property = _mapper.Map<Property>(propertyData);
        //        _context.Properties.Add(property);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertyServiceException("Failed to add property.", ex);
        //    }
        //}


        public async Task AddProperty(PropertyDTO propertyData)
        {
            try
            {
                var property = _mapper.Map<Property>(propertyData);
                if (!Enum.IsDefined(typeof(PropertyStatus), property.Status))
                {
                    throw new ArgumentException("Invalid proeprty status .");
                }
                _context.Properties.Add(property);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add property", ex);
            }
        }

        public async Task<PropertyDTO> GetPropertyById(int propertyId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);
                if (property == null)
                    throw new NotFoundException($"Property with ID {propertyId} not found.");
                return _mapper.Map<PropertyDTO>(property);
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to retrieve property by ID.", ex);
            }
        }

        public async Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            try
            {
                var properties = await _context.Properties.ToListAsync();
                return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to retrieve all properties.", ex);
            }
        }

        public async Task<bool> UpdateProperty(PropertyDTO propertyData)
        {
            try
            {
                var existingProperty = await _context.Properties.FindAsync(propertyData.PropertyId);
                if (existingProperty == null)
                    throw new NotFoundException($"Property with ID {propertyData.PropertyId} not found.");
                _mapper.Map(propertyData, existingProperty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to update property.", ex);
            }
        }

        public async Task<PropertyStatus> GetPropertyStatus(int propertyId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);
                if (property == null)
                    throw new NotFoundException($"Property with ID {propertyId} not found.");
                return property.Status;
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to retrieve property status.", ex);
            }
        }

        //public Task<IEnumerable<FlatType?>> GetFlatTypes()
        //{
        //    try
        //    {
        //        var flatType = Enum.GetValues(typeof(FlatType)).Cast<FlatType?>();
        //        return Task.FromResult<IEnumerable<FlatType?>>(flatType);

        //        // return Enum.GetValues(typeof(FlatType)).Cast<FlatType?>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertyServiceException("Failed to retrieve flat types.", ex);
        //    }
        //}

        public async Task<bool> DeleteProperty(int propertyId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);
                if (property == null)
                    throw new NotFoundException($"Property with ID {propertyId} not found.");

                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to delete property.", ex);
            }
        }

        public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByCity(string city)
        {
            try
            {
                var properties = await _context.Properties.Where(p => p.City == city).ToListAsync();
                return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
            }
            catch (Exception ex)
            {
                throw new PropertyServiceException("Failed to search properties by city.", ex);
            }
        }

        //public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByPrice(decimal? minPrice, decimal? maxPrice)
        //{
        //    try
        //    {
        //        var properties = await _context.Properties
        //            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertyServiceException("Failed to search properties by price range.", ex);
        //    }
        //}

        //public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByType(PropertyType? type)
        //{
        //    try
        //    {
        //        var properties = await _context.Properties
        //            .Where(p => p.Type == type)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertyServiceException("Failed to search properties by type.", ex);
        //    }
        //}

        //public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByFlat(FlatType? flatType)
        //{
        //    try
        //    {
        //        var properties = await _context.Properties
        //            .Where(p => p.FlatType == flatType)
        //            .ToListAsync();

        //        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PropertyServiceException("Failed to search properties by flat type.", ex);
        //    }
        //}
    }

    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PropertyServiceException : Exception
    {
        public PropertyServiceException()
        {
        }

        public PropertyServiceException(string? message) : base(message)
        {
        }

        public PropertyServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PropertyServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

    
