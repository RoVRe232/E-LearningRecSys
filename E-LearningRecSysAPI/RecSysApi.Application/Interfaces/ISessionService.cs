using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface ISessionService
    {
        public Task<User> GetUserFromLoginAsync(LoginData login);
        public Task<UserDetailsDTO> GetUserDetailsAsync(Guid userId);
        public Task<AuthenticatedUserDTO> GetAuthenticatedUserFromLoginAsync(LoginDTO login);
        public Task<bool> CreateUnconfirmedUserAsync(SignupDTO signupData);
        public Task<bool> CreateUnconfirmedAdminAsync(SignupDTO signupData, string confirmationToken);
        public Task<RefreshedAuthTokensDTO> GetRefreshTokenForAuthenticatedUser(Guid userId, string refreshToken);
    }
}
