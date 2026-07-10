using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace ProductManagement.IntegrationTests
{
    public class AuthIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_Should_Return_OK()
        {
            var request = new
            {
                UserName = $"integrationuser{Guid.NewGuid():N}",
                Email = $"{Guid.NewGuid():N}@test.com",
                Password = "Password@123"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/Auth/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Login_Should_Return_AccessToken()
        {
            // Arrange
            var registerRequest = new
            {
                UserName = "loginuser",
                Email = "login@test.com",
                Password = "Password@123"
            };

            await _client.PostAsJsonAsync("/api/v1/Auth/register", registerRequest);

            var loginRequest = new
            {
                Email = "login@test.com",
                Password = "Password@123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/Auth/login", loginRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();

            result.Should().Contain("accessToken");
            result.Should().Contain("refreshToken");
        }
    }
}
