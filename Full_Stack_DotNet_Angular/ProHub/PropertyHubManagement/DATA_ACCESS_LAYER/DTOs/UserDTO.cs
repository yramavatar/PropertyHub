using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA_ACCESS_LAYER.Data_Models;
using DATA_ACCESS_LAYER.Models;

namespace DATA_ACCESS_LAYER.DTOs
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName {  get; set; }
        public string lastName { get; set; } = string.Empty;
        public string contactNumber { get; set; }
       public UserType userType { get; set; }
        //public enum UserType
        //{
        //    [Display(Name = "Buyer")]
        //    Buyer,
        //    [Display(Name = "PropertyOwner")]
        //    PropertyOwner

        //}
        // public string UserType { get; set; }
    }
}
