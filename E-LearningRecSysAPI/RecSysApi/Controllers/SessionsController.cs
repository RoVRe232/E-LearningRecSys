using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Dtos.Http;
using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticatedUserDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticatedUserDTO>> Login([FromBody] LoginDTO loginData)
        {
            var authenticatedUser = (await _sessionService.GetAuthenticatedUserFromLoginAsync(loginData));
            if (authenticatedUser != null)
            {
                var response = new BasicHttpResponseDTO<AuthenticatedUserDTO>
                {
                    Success = true,
                    Errors = new List<string>(),
                    Result = authenticatedUser
                };
                return Ok(response);
            }

            return BadRequest("Login failed due to invalid email or password!");
        }

        [HttpPost]
        [Route("refreshToken")]
        [Authorize(Policy = "RefreshOnly")]
        public async Task<ActionResult<RefreshedAuthTokensDTO>> RefreshToken()
        {
            var authorizationToken = HttpContext.Request.Headers["Authorization"];
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var newAuthTokens = (await _sessionService.GetRefreshTokenForAuthenticatedUser(new Guid(claim.Value), authorizationToken));

                    var response = new BasicHttpResponseDTO<RefreshedAuthTokensDTO>
                    {
                        Success = true,
                        Errors = new List<string>(),
                        Result = newAuthTokens
                    };
                    return Ok(response);
                }

            } while (claimsIdentiy.MoveNext());

            return BadRequest("RefreshFailed");
        }

        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Signup(SignupDTO signupData)
        {
            bool result = await _sessionService.CreateUnconfirmedUserAsync(signupData);
            if(result)
                return Ok(true);

            return BadRequest("Signup failed due to invalid signup data");
        }

        [HttpPost]
        [Authorize(Policy = "AuthOnly")]
        [Route("authenticatedUserDetails")]
        public async Task<ActionResult<UserDetailsDTO>> UserDetails(UserDetailsDTO userDetails)
        {
            return Ok("Authenticated");
        }

        [HttpGet]
        [Authorize(Policy = "AuthOnly")]
        [Route("authenticatedUserDetails")]
        public async Task<ActionResult<UserDetailsDTO>> UserDetails()
        {
            //TODO ADD CLAIMS EXTRACTOR MIDDLEWARE
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if(claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await _sessionService.GetUserDetailsAsync(new Guid(claim.Value));
                    var response = new BasicHttpResponseDTO<UserDetailsDTO>
                    {
                        Success = true,
                        Errors = new List<string>(),
                        Result = userDetails
                    };
                    return Ok(response);
                }
                
            } while (claimsIdentiy.MoveNext());

            return BadRequest("Not found");
        }
    }
}
