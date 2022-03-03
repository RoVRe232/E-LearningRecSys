using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokenConfigurationDTO _tokenConfiguration;
        public AuthService(IOptions<TokenConfigurationDTO> tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration.Value;
        }
        public JwtDTO GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            });

            var expirationDate = DateTime.UtcNow.AddHours(12);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _tokenConfiguration.Audience,
                Issuer = _tokenConfiguration.Issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtDTO { Token = tokenHandler.WriteToken(token), ExpirationDate = expirationDate };
        }


    }
}
