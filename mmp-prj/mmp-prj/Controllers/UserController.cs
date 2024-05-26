using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mmp_prj.Models;
using mmp_prj.Service;

namespace mmp_prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }

            var addedUser = _userService.AddUser(user);
            if (addedUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add user.");
            }

            return Ok(addedUser);
        }

        [HttpDelete("DeleteUser/{email}")]
        public IActionResult DeleteUser(string email)
        {
            var deletedUser = _userService.DeleteUser(email);
            if (deletedUser == null)
            {
                return NotFound($"User with email '{email}' not found.");
            }

            return Ok(deletedUser);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound($"User with email '{email}' not found.");
            }

            return Ok(user);
        }

        [HttpPut("UpdateUser/{email}")]
        public IActionResult UpdateUser(string email, [FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }

            var updatedUser = _userService.UpdateUser(email, user);
            if (updatedUser == null)
            {
                return NotFound($"User with email '{email}' not found.");
            }

            return Ok(updatedUser);
        }
        [HttpGet("GetUsersByRole/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            var users = _userService.GetUsersByRole(role);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

    }
}
