using System;
using System.Threading.Tasks;
using BukashkaCo.Finance.Application.Services;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Entities;
using BukashkaCo.Finance.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace BukashkaCo.Finance.Tests
{
    public class RegistrationServiceTests
    {
        [Test]
        public async Task Register_ShouldReturnUserModel()
        {
            //arrange
            var userName = "Test";
            var email = "Test@gmail.com";
            var password = "Password123$";
            var user = new User {UserName = userName, Email = email};

            var mockStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockStore.Object,  null, 
                null, null, null, null, null, null, null);

            mockUserManager.Setup(x => 
                    x.CreateAsync(It.IsAny<User>(), password))
                    .Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(x => 
                    x.FindByNameAsync(userName))
                    .Returns(Task.FromResult(user));

            var mockJwtGenerator = new Mock<IJwtGenerator>();
            mockJwtGenerator.Setup(x => x.CreateToken(It.IsAny<User>()))
                .Returns("token");
            
            var service = new RegistrationService(mockUserManager.Object, mockJwtGenerator.Object);
            
            var registerRequest = new RegisterRequest
            {
                UserName = userName,
                Email = email,
                Password = password
            };
            
            //act
            var result = await service.Register(registerRequest);

            //assert
            mockJwtGenerator.Verify(x => x.CreateToken(It.IsAny<User>()), Times.Once);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(userName, result.UserName);
            Assert.AreEqual(email, result.Email);
            Assert.IsNotNull(result.Token);
            Assert.IsNotEmpty(result.Token);
        }
    }
}