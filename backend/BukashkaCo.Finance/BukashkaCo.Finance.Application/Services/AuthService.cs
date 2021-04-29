using System;
using System.Threading.Tasks;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Entities;
using BukashkaCo.Finance.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BukashkaCo.Finance.Application.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        
        public async Task<UserModel> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new Exception("Not Found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                return new UserModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            throw new Exception("Not Found");
        }
    }
}