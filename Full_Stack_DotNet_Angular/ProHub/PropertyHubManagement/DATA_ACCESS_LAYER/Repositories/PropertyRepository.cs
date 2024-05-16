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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyHubDbContext _context;
        private readonly IMapper _mapper;

        public PropertyRepository(PropertyHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      

        public async Task AddProperty(PropertyDTO propertyData)
        {
            try
            {
                var property = _mapper.Map<Property>(propertyData);
                if(!Enum.IsDefined(typeof(PropertyStatus), property.Status))
                {
                    throw new ArgumentException("Inavkid proeprty status .");
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
                return _mapper.Map<PropertyDTO>(property);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve property by ID", ex);
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
                throw new Exception("Failed to retrieve all properties", ex);
            }
        }

        public async Task<bool> UpdateProperty(PropertyDTO propertyData)
        {
            try
            {
                var existingProperty = await _context.Properties.FindAsync(propertyData.PropertyId);
                if (existingProperty == null)
                    return false;

                _mapper.Map(propertyData, existingProperty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update property", ex);
            }
        }

        public async Task<PropertyStatus> GetPropertyStatus(int propertyId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);
                return property?.Status ?? PropertyStatus.Available;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve property status", ex);
            }
        }

        public async Task<IEnumerable<FlatType?>> GetFlatTypes()
        {
            try
            {
                var flatTypes = await _context.Properties.Select(p => p.FlatType).Distinct().ToListAsync();
                return flatTypes;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve flat types", ex);
            }
        }

        public async Task<bool> DeleteProperty(int propertyId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);
                if (property == null)
                    return false;

                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete property", ex);
            }
        }

        public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByCity(string city)
        {
            try
            {
                var properties = await _context.Properties
                    .Where(p => p.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();
                return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to search properties by city", ex);
            }
        }

    }
}




// Oleer oen
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
//        throw new Exception("Failed to add property", ex);
//    }
//}



//public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByPrice(decimal? minPrice, decimal? maxPrice)
//{
//    try
//    {
//        var properties = await _context.Properties
//            .Where(p => (!minPrice.HasValue || p.Price >= minPrice) &&
//                        (!maxPrice.HasValue || p.Price <= maxPrice))
//            .ToListAsync();
//        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Failed to search properties by price range", ex);
//    }
//}

//public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByType(PropertyType? type)
//{
//    try
//    {
//        var properties = await _context.Properties
//            .Where(p => !type.HasValue || p.Type == type)
//            .ToListAsync();
//        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Failed to search properties by type", ex);
//    }
//}

//public async Task<IEnumerable<PropertyDTO>> SearchPropertiesByFlat(FlatType? flatType)
//{
//    try
//    {
//        var properties = await _context.Properties
//            .Where(p => !flatType.HasValue || p.FlatType == flatType)
//            .ToListAsync();
//        return _mapper.Map<IEnumerable<PropertyDTO>>(properties);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Failed to search properties by flat type", ex);
//    }
//}