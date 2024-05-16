using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Data_Models;
using System.ComponentModel.DataAnnotations;

namespace DATA_ACCESS_LAYER.DTOs
{
    public class PropertyDTO
    {
        public int PropertyId { get; set; }
        public int OwnerId { get; set; }


         public PropertyType ? PropertyType { get; set; }

       //public string? eaerlier it was PropertyType { get; set; }



           public FlatType? FlatType { get; set; }
      
        //public string FlatType { get; set; }
        public int? SizeSqFt { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
      public PropertyStatus PropertyStatus { get; set; }
     //   public string : earlier it was PropertyStatus { get; set; }
        //public decimal? MinPrice { get; set; }
        //public decimal? MaxPrice { get; set; }

        public string ImageUrl { get; set; }
    }
}
