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
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.Users;
        }

        public Task<bool> CreateUnconfirmedAdminAsync(SignupDTO signupData, string confirmationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUnconfirmedUserAsync(SignupDTO signupData)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticatedUserDTO> GetAuthenticatedUserFromLoginAsync(LoginDTO login)
        {
            var user = await _userRepository
                .FirstOrDefaultAsync(e => e.Email == login.Email && e.Password == login.Password);
            var account = user.Account;

            return new AuthenticatedUserDTO
            {
                UserID = user.UserID,
                AccountID = account.AccountID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccountName = account.Name,
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
