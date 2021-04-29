using System.Collections.Generic;
using System.Linq;
using BukashkaCo.Finance.Application.Services;
using BukashkaCo.Finance.Domain.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BukashkaCo.Finance.Tests
{
    public class JwtGeneratorTests
    {
        [Test]
        public void CreateToken_ShouldReturnToken()
        {
            //arrange
            var user = new User {UserName = "Alex", Email = "alex@gmail.com"};
            
            var memorySettings = new Dictionary<string, string> {
                {"JwtKey", "SuperSuperSecretKey"},
            };
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(memorySettings)
                .Build();

            var service = new JwtGenerator(configuration);

            //act
            var result = service.CreateToken(user);

            //assert
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(2, result.ToCharArray().Where(x => x == '.').Count());
            
        }
    }
}