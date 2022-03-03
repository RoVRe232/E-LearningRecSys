using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Interfaces;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/sessions")]
    public class SessionsController : ApiBaseController
    {
        private ISessionService _sessionService;
        private IAuthService _authService;
        public SessionsController(ISessionService sessionService, IAuthService authService)
        {
            _sessionService = sessionService;
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JwtDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<JwtDTO>> Login(LoginDTO loginData)
        {
            var authenticatedUser = (await _sessionService.GetAuthenticatedUserFromLoginAsync(loginData));
            if(authenticatedUser != null)
                return Ok(_authService.GenerateToken(authenticatedUser));

            return BadRequest("Login failed due to invalid email or password!");
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<bool>> Signup(SignupDTO signupData)
        {
            bool result = await _sessionService.CreateUnconfirmedUserAsync(signupData);
            if(result)
                return Ok(true);

            return BadRequest("Signup failed due to invalid signup data");
        }

        [HttpPost]
        [Route("authenticatedUserDetails")]
        [Authorize]
        public async Task<ActionResult<AuthenticatedUserDTO>> UserDetails(LoginDTO loginData)
        {
            var authenticatedUser = (await _sessionService.GetAuthenticatedUserFromLoginAsync(loginData));
            if(authenticatedUser != null)
                return Ok(authenticatedUser);
            return BadRequest("");
        }
    }
}
