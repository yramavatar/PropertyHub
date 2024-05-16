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
  public interface IUserRepository
    {
        Task<UserDTO> RegisterUser(UserDTO userData);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> GetUserByEmail(string email);
        //  original one: Task<UserDTO> AuthenticateUser(string email, string password);

        Task<UserDTO> AuthenticateUser(string email, string password, UserType userType);

        Task<bool> DeleteUser(int userId);
        Task<UserDTO> UpdateUser(UserDTO userData);

    }
}
