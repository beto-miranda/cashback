using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CashBack.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CashBack.Integration.Tests
{
    public class LoginTests : IClassFixture<CustomWebApplicationFactory<CashBack.Web.Startup>>
    {
        private readonly CustomWebApplicationFactory<CashBack.Web.Startup> factory;
        public LoginTests(CustomWebApplicationFactory<CashBack.Web.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task LoginUnauthorized()
        {
            // Arrange
            var client = factory.CreateClient();

            var loginModel = new LoginModel()
            {
                UserName = "user@outlook.com",
                Password = "password"
            };
            var c = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("api/auth/login", c);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

}