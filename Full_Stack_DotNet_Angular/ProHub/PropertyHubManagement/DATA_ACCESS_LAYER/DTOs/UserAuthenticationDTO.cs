using DATA_ACCESS_LAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.DTOs
{
   public class UserAuthenticationDTO
    {
        public string email {  get; set; }
        public string password { get; set; }

        public UserType userType { get; set; }
 
    }
}
