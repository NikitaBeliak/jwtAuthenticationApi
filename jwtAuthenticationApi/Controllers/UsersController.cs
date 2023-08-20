using Domain.Entitys.Interfaces;
using Domain.Models;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace jwtAuthenticationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userRepository.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }
    }
}
