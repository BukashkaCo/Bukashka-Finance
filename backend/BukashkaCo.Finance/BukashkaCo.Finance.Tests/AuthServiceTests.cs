using System.Threading.Tasks;
using BukashkaCo.Finance.Application.Services;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Entities;
using BukashkaCo.Finance.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace BukashkaCo.Finance.Tests
{
    public class AuthServiceTests
    {
        [Test]
        public async Task Login_ShouldRetutnUserModel()
        {
            //arrange
            var expectedUserName = "Test";
            var expectedEmail = "Test@gmail.com";
            var password = "Test43214$";
            var expectedUser = new User {UserName = expectedUserName, Email = expectedEmail};

            var mockStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockStore.Object,  null, 
                null, null, null, null, null, null, null);

            mockUserManager.Setup(userManager => 
                    userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(userManager => 
                    userManager.FindByNameAsync(expectedUserName))
                .Returns(Task.FromResult(expectedUser));

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object, contextAccessor.Object, 
                userPrincipalFactory.Object, null, null, null);

            mockSignInManager.Setup(signInManager =>
                signInManager.CheckPasswordSignInAsync(expectedUser, password, false))
                .Returns(Task.FromResult<SignInResult>(SignInResult.Success));

            var mockJwtGenerator = new Mock<IJwtGenerator>();
            mockJwtGenerator.Setup(x => x.CreateToken(expectedUser))
                .Returns("token");
            
            var service = new AuthService(mockUserManager.Object, mockSignInManager.Object,
                mockJwtGenerator.Object);
            
            var loginRequest = new LoginRequest
            {
                UserName = expectedUserName,
                Password = password
            };
            
            //act
            var result = await service.Login(loginRequest);

            //assert
            mockJwtGenerator.Verify(x => x.CreateToken(expectedUser), Times.Once);
            
            Assert.IsNotNull(result);
            
            Assert.IsNotNull(result.Token);
            Assert.IsNotEmpty(result.Token);
            
            Assert.AreEqual(expectedEmail, result.Email);
            Assert.AreEqual(expectedUserName, result.UserName);
        }
        
    }
}