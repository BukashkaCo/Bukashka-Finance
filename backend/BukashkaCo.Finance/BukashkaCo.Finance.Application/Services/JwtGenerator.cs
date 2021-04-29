using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BukashkaCo.Finance.Application.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        public JwtGenerator(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtKey"]));
        }
        private readonly SymmetricSecurityKey _key;
        
        public string CreateToken(User user)
        {
            var credentials =  new SigningCredentials(_key, 
                SecurityAlgorithms.HmacSha256Signature);

            var descriptor = new SecurityTokenDescriptor
            {   
                Audience = "TODO",
                Expires = DateTime.Now.AddDays(2),
                Claims = new Dictionary<string, object>
                {
                    {"Username", user.UserName}   
                },
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}