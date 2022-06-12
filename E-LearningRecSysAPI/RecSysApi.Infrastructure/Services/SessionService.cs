using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Tokens;
using RecSysApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthService _authService;
        public SessionService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Users;
            _accountRepository = _unitOfWork.Accounts;
            _authService = authService;
        }

        public Task<bool> CreateUnconfirmedAdminAsync(SignupDTO signupData, string confirmationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateUnconfirmedUserAsync(SignupDTO signupData)
        {
            var user = await _userRepository
                .FindAsync(e => e.Email == signupData.Email);
            if (user != null)
                return false;
            user = await _userRepository.AddAsync(new User
            {
                UserID = Guid.NewGuid(),
                Role = "User",
                FirstName = signupData.FirstName,
                LastName = signupData.LastName,
                Email = signupData.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(signupData.Password),
                IsConfirmed = false,
                Phone = signupData.Phone,
                AddressLine1 = signupData.AddressLine1,
                AddressLine2 = signupData.AddressLine2,
                Country = signupData.Country,
                City = signupData.City,
                State = signupData.State,
                PostalCode = signupData.PostalCode
                
            });
            var account = await _accountRepository
                .FindAsync(e => e.Name == signupData.AccountName);
            if (account == null)
            {
                account = await _accountRepository.AddAsync(new Account
                {
                    AccountID = Guid.NewGuid(),
                    Name = signupData.AccountName,
                });
                account.Users.Add(user);
                user.AccountID = account.AccountID;
                user.Account = account;
            }
            else
            {
                if (string.IsNullOrEmpty(signupData.AccountSignupInvitationToken))
                    return false;
                var signupInvitation = account.AccountSignupInvitations
                    .First(e => e.Token == signupData.AccountSignupInvitationToken);
                if (signupInvitation == null || signupInvitation.ExpirationDate.CompareTo(DateTime.UtcNow) <= 0)
                    return false;

                user.Account = account;
                user.AccountID = account.AccountID;
                account.Users.Add(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<AuthenticatedUserDTO> GetAuthenticatedUserAsync(Guid userId)
        {
            var user = await _userRepository
               .GetUserWithAccountByExpressionAsync(e => e.UserID == userId);

            var authenticatedUser = new AuthenticatedUserDTO
            {
                UserID = user.UserID,
                AccountID = user.AccountID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                AuthToken = _authService.GenerateToken(user),
                RefreshToken = _authService.GenerateToken(user, true)
            };

            user.ActiveRefreshToken = new JwtToken
            {
                Token = authenticatedUser.RefreshToken.Token,
                ExpirationDate = authenticatedUser.RefreshToken.ExpirationDate
            };

            if (user != null)
            {
                await _unitOfWork.SaveChangesAsync();
                return authenticatedUser;
            }

            throw new ApplicationException("User does not exist");
        }

        public async Task<AuthenticatedUserDTO> GetAuthenticatedUserFromLoginAsync(LoginDTO login)
        {
            var user = await _userRepository
                .GetUserWithAccountByExpressionAsync(e => e.Email == login.Email);

            var authenticatedUser = new AuthenticatedUserDTO
            {
                UserID = user.UserID,
                AccountID = user.AccountID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                AuthToken = _authService.GenerateToken(user),
                RefreshToken = _authService.GenerateToken(user, true)
            };

            user.ActiveRefreshToken = new JwtToken{
                Token = authenticatedUser.RefreshToken.Token,
                ExpirationDate = authenticatedUser.RefreshToken.ExpirationDate
            };

            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                await _unitOfWork.SaveChangesAsync();
                return authenticatedUser;
            }

            throw new ApplicationException("Invalid username or password or user does not exist");
        }

        public async Task<RefreshedAuthTokensDTO> GetRefreshTokenForAuthenticatedUser(Guid userId, string refreshToken)
        {
            var user = await _userRepository
                .GetUserWithTokensFamilyAsync(e => e.UserID == userId);
            if (user != null)
            {
                //if(user.ActiveRefreshToken == null || user.ActiveRefreshToken.Token != refreshToken.Split(' ')[1])
                //{
                    //TODO Invalidate refresh token family and force user to log in again
                    //if(user.ActiveRefreshToken != null)
                    //    user.UsedRefreshTokensFamily.Add(user.ActiveRefreshToken);
                    //user.ActiveRefreshToken = null;

                    //await _unitOfWork.SaveChangesAsync();

                    //throw new ApplicationException("Refresh token invalidated");
                //}
                //else
                {
                    var refreshedAuthTokens = new RefreshedAuthTokensDTO
                    {
                        AuthToken = _authService.GenerateToken(user),
                        RefreshToken = _authService.GenerateToken(user, true)
                    };

                    user.UsedRefreshTokensFamily.Add(user.ActiveRefreshToken);
                    user.ActiveRefreshToken = new JwtToken
                    {
                        Token = refreshedAuthTokens.RefreshToken.Token,
                        ExpirationDate = refreshedAuthTokens.RefreshToken.ExpirationDate
                    };
                    await _unitOfWork.SaveChangesAsync();

                    return refreshedAuthTokens;
                }
            }

            throw new ApplicationException("Invalid username or password or user does not exist");
        }

        public async Task<AuthenticatedUserDTO> GetAuthenticatedUserFromClaimsAsync(IEnumerator<Claim> claimsIdentiy)
        {
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await GetAuthenticatedUserAsync(new Guid(claim.Value));
                    return userDetails;
                }

            } while (claimsIdentiy.MoveNext());
            return null;
        }

        public async Task<UserDetailsDTO> GetUserDetailsAsync(Guid userId)
        {
            var user = await _userRepository
                .GetUserWithAccountByExpressionAsync(e => e.UserID == userId);
            return new UserDetailsDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccountName = user.Account.Name,
                Email = user.Email,
                Phone = user.Phone,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Country = user.Country,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode
            };
        }

        public Task<User> GetUserFromLoginAsync(LoginData login)
        {
            throw new NotImplementedException();
        }
    }
}
