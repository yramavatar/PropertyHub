using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC_LAYER.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //public async Task<UserDTO> RegisterUser(UserDTO userData)
        //{

        //    try
        //    {
        //        // Validate and register user using repository
        //        var registeredUser = await _userRepository.RegisterUser(userData);
        //        return _mapper.Map<UserDTO>(registeredUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while registering user.", ex);
        //    }
        //}
        public async Task<UserDTO> RegisterUser(UserDTO userData)
        {
            try
            {
                // Check if username is unique
                var existingUsername = await _userRepository.GetUserByEmail(userData.email);
                if (existingUsername != null)
                {
                    throw new Exception("Email is already registered.");
                }

                // Check if email is unique and has the correct format
                 



                var userEntity = _mapper.Map<UserDTO>(userData);
                //save user to Database
                var registeredUser = await _userRepository.RegisterUser(userEntity);
                //Map User entity back to UserDto

                var registeredUserDTO = _mapper.Map<UserDTO>(registeredUser);
                return registeredUserDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while registering user.", ex);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users= await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            // Call repository method to get user by ID
            var user = await _userRepository.GetUserById(userId);

            // Map User model to UserDTO
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            // Call repository method to get user by email
            var user = await _userRepository.GetUserByEmail(email);

            // Map User model to UserDTO
            return _mapper.Map<UserDTO>(user);
        }

        //public async Task<UserDTO> AuthenticateUser(string email, string password)
        //{

        //    try
        //    {
        //        // Authenticate user using repository
        //        var authenticatedUser = await _userRepository.AuthenticateUser(email, password);
        //        return _mapper.Map<UserDTO>(authenticatedUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while authenticating user.", ex);
        //    }

        //}


        // original
        //public async Task<UserDTO> AuthenticateUser(string email, string password)
        //{
        //    try
        //    {
        //        var userEntity = await _userRepository.AuthenticateUser(email, password);
        //        if (userEntity == null)
        //        {
        //            throw new NotFoundException("User not found.");
        //        }
        //        return _mapper.Map<UserDTO>(userEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UserServiceException("Error occurred while authenticating user.", ex);
        //    }
        //}

        public async Task<UserDTO> AuthenticateUser(string email, string password, UserType userType)
        {
            try
            {
                var authenticatedUser = await _userRepository.AuthenticateUser(email, password, userType);
                return _mapper.Map<UserDTO>(authenticatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Earror occured while authenticating user.", ex);
            }
        }

        //it was earlier :public async Task<UserDTO> AuthenticateUser(string email, string password)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetUserByEmail(email);

        //        if( user !=null && user.password == password)
        //        {
        //            return _mapper.Map<UserDTO>(user);
        //        }
        //          else
        //        {
        //            return null;
        //        }
        //    }
        //     catch (Exception ex)
        //    {
        //        throw new Exception("Earror occured while authenticating user.", ex);
        //    }
        //}

        public async Task<bool> DeleteUser(int userId)
        {
            // Call repository method to delete user
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userData)
        {
            // Map UserDTO to User model
            var user = _mapper.Map<User>(userData);

            // Call repository method to update user
            var updatedUser = await _userRepository.UpdateUser(userData);

            // Map the returned User model back to UserDTO
            return _mapper.Map<UserDTO>(updatedUser);
        }
    }

    [Serializable]
    public class UserServiceException : Exception
    {
        public UserServiceException()
        {
        }

        public UserServiceException(string? message) : base(message)
        {
        }

        public UserServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
