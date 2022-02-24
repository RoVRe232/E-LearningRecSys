using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Interfaces;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    public class SessionsController : ApiBaseController
    {
        private ISessionService _sessionService;
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticatedUserDTO>> Login(LoginDTO loginData)
        {
            var authenticatedUser = (await _sessionService.GetAuthenticatedUserFromLoginAsync(loginData));
            if(authenticatedUser != null)
                return Ok(authenticatedUser);

            return BadRequest("Login failed due to invalid email or password!");
        }
    }
}
