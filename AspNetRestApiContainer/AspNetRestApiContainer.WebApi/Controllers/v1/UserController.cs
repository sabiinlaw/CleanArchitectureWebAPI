using AspNetRestApiContainer.Application.Parameters.Auth;
using AspNetRestApiContainer.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// POST: api/controller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GeByUserName(request.Login);
            if (user == null)
                return BadRequest("Bad credentials");

            if (user.Password != request.Password)
                return BadRequest("Bad credentials");

            var accessToken = TokenService.CreateToken(user);
            return Ok(accessToken);
        }
    }
}
