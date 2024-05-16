using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUser(UserDTO userData);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> GetUserByEmail(string email);
        //it was earlier : Task<UserDTO> AuthenticateUser(string email, string password);
        Task<UserDTO> AuthenticateUser(string email, string password, UserType userType);
        Task<bool> DeleteUser(int userId);
        Task<UserDTO> UpdateUser(UserDTO userData);
    }
}
