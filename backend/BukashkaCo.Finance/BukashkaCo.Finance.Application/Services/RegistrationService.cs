using System;
using System.Threading.Tasks;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Entities;
using BukashkaCo.Finance.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BukashkaCo.Finance.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        public RegistrationService(UserManager<User> userManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }
        
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        
        public async Task<UserModel> Register(RegisterRequest request)
        {
            var user = new User {UserName = request.UserName, Email = request.Email};

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new UserModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            var totalErrors = "";
            foreach (var error in result.Errors)
            {
                totalErrors += error.Description;
            }
            
            throw new Exception($"Failed to register: {totalErrors}");
        }
    }
}