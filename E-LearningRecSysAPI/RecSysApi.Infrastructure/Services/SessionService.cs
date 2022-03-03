using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Users;
            _accountRepository = _unitOfWork.Accounts;
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

        public async Task<User> GetAuthenticatedUserFromLoginAsync(LoginDTO login)
        {
            var user = await _userRepository
                .GetUserWithAccountByExpressionAsync(e => e.Email == login.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return user;
            }

            throw new ApplicationException("Invalid username or password or user does not exist");
        }

        public Task<User> GetUserFromLoginAsync(LoginData login)
        {
            throw new NotImplementedException();
        }
    }
}
