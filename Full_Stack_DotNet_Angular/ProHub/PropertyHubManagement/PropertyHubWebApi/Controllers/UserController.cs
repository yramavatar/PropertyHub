using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using BUSINESS_LOGIC_LAYER.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DATA_ACCESS_LAYER.Models;

namespace PropertyHubWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDTO userData)
        {
            try
            
            {
                var registeredUser = await _userService.RegisterUser(userData);
                //return Ok(registeredUser);

                return Ok(new {User= registeredUser,Message="User registered"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                    return NotFound("User not found");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);
                if (user == null)
                    return NotFound("User not found");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //it was eaerliwere :[HttpPost("authenticate")]
        //public async Task<IActionResult> AuthenticateUser(string email, string password)
        //{
        //    try
        //    {
        //        var authenticatedUser = await _userService.AuthenticateUser(email, password);
        //        if (authenticatedUser == null)
        //            return Unauthorized("Invalid email or password");
        //        var authenticatedUserDto = _mapper.Map<UserDTO>(authenticatedUser);
        //         return Ok(authenticatedUserDto);
        //        //return Ok("Authentication was successfull.");
        //        // Updated Return userDTO along with messgae
        //        //  return Ok(new { Message = "Authentication was successful",User = //authenticatedUserDto});
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}




        [HttpPost("authenticate")]
        
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationDTO userAuthentication)
        {
            try
            {
                var authenticatedUser = await _userService.AuthenticateUser(userAuthentication.email, userAuthentication.password, userAuthentication.userType);
                if (authenticatedUser == null)
                    return Unauthorized("Invalid email or password");
               // return Ok(new { message = "Login Successful" });

                // var authenticatedUserDto = _mapper.Map<UserDTO>(authenticatedUser);
                  return Ok(authenticatedUser);

                //return Ok("Authentication was successfull.");
                // Updated Return userDTO along with messgae
                //  return Ok(new { Message = "Authentication was successful",User = //authenticatedUserDto});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var deleted = await _userService.DeleteUser(userId);
                if (!deleted)
                    return NotFound("User not found");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserDTO userData)
        {
            try
            {
                if (userId != userData.userId)
                    return BadRequest("User ID mismatch");

                var updatedUser = await _userService.UpdateUser(userData);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
