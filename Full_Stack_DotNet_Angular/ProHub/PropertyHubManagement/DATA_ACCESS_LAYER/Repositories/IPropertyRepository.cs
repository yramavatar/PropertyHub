using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Repositories
{
    public interface IPropertyRepository
    {
        Task AddProperty(PropertyDTO propertyData);
        Task<PropertyDTO> GetPropertyById(int propertyId);
        Task<IEnumerable<PropertyDTO>> GetAllProperties();
        Task<bool> UpdateProperty(PropertyDTO propertyData);
        Task<PropertyStatus> GetPropertyStatus(int propertyId);
      
        Task<bool> DeleteProperty(int propertyId);
        Task<IEnumerable<PropertyDTO>> SearchPropertiesByCity(string city);
        
    }
}



// Task<IEnumerable<PropertyDTO>> SearchPropertiesByPrice(decimal? minPrice, decimal? maxPrice);
//Task<IEnumerable<PropertyDTO>> SearchPropertiesByType(PropertyType? type);
//Task<IEnumerable<PropertyDTO>> SearchPropertiesByFlat(FlatType? flatType);
// Task<IEnumerable<FlatType?>> GetFlatTypes();