using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using System.Threading.Tasks;
using WebApi.Models;
using System.Linq;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetCurrentUserInfo()
        {
            var claims = User.Claims.Select(x => new { ClaimName = x.Type, Value = x.Value });

            return Ok(claims);
        }


        [HttpGet("protected")]
        [Authorize(Roles = "admins")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }
    }
}
