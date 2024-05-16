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
    public class UserRepository : IUserRepository
    {
        private readonly PropertyHubDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(PropertyHubDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        //try
        //{
        //    var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userData.Email);
        //    if (existingUser != null)
        //    {
        //        throw new Exception("User with this email already exists.");
        //    }

        //    var userEntity = _mapper.Map<User>(userData);
        //    _dbContext.Users.Add(userEntity);
        //    await _dbContext.SaveChangesAsync();

        //    return _mapper.Map<UserDTO>(userEntity);
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("Error occurred while registering user.", ex);
        //}

        // Check if password is unique
        //var existingPassword = await _dbContext.Users.FirstOrDefaultAsync(u => u.Password == userData.Password);
        //if (existingPassword != null)
        //{
        //    throw new Exception("Password is already in use.");
        //}
        public async Task<UserDTO> RegisterUser(UserDTO userData)
        {
            try
            {
                // Check if username is unique
                var existingUsername = await _dbContext.Users.FirstOrDefaultAsync(u => u.username == userData.username);
                if (existingUsername != null)
                {
                    throw new Exception("Username is already taken.");
                }

                // Check if email is unique and has the correct format
                var existingEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == userData.email);
                if (existingEmail != null)
                {
                    throw new Exception("Email is already registered.");
                }
                if (!userData.email.EndsWith("@gmail.com"))
                {
                    throw new Exception("Email must be a Gmail address.");
                }



                var userEntity = _mapper.Map<User>(userData);
                _dbContext.Users.Add(userEntity);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while registering user.", ex);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving all users.", ex);
            }
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            try
            {
                var userEntity = await _dbContext.Users.FindAsync(userId);
                return _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user by ID.", ex);
            }
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            try
            {
                var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == email);
                return _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user by email.", ex);
            }
        }

        // it was earlier : public async Task<UserDTO> AuthenticateUser(string email, string password)
        //{
        //    try
        //    {
        //        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);
        //        return _mapper.Map<UserDTO>(userEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while authenticating user.", ex);
        //    }
        //}

        public async Task<UserDTO> AuthenticateUser(string email, string password, UserType userType)
        {
            try
            {
                var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password && u.userType == userType);
                return _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while authenticating user.", ex);
            }
        }
        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var userEntity = await _dbContext.Users.FindAsync(userId);
                if (userEntity == null)
                {
                    throw new Exception("User not found.");
                }

                _dbContext.Users.Remove(userEntity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting user.", ex);
            }
        }

        public async Task<UserDTO> UpdateUser(UserDTO userData)
        {
            try
            {
                var userEntity = await _dbContext.Users.FindAsync(userData.userId);
                if (userEntity == null)
                {
                    throw new Exception("User not found.");
                }

                _mapper.Map(userData, userEntity);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating user.", ex);
            }
        }
    }
}
