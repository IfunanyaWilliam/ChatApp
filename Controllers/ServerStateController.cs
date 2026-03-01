using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerStateController : ControllerBase
    {
        private readonly IUserService _userService;


        public ServerStateController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("Status")]
        public IActionResult GetServerState()
        {
            var users = _userService.GetServerState();
            return Ok(users);
        }
    }
    
}
