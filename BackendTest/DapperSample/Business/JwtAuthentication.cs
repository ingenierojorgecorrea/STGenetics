using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using STGenetics.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace STGenetics.Business
{
    public class JwtAuthentication
    {
        public string GenerateToken(IConfiguration configuration)
        {
            string token = string.Empty;
            if (configuration != null)
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Sid, configuration.GetSection("Jwt:User").Value)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

                token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            return token.ToString();
        }
    }
}
