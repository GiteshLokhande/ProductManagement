using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace ProductManagement.IntegrationTests
{
    public class ProductIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_Should_Return_OK()
        {
            await AuthenticateAsync();

            var response = await _client.GetAsync("/api/v1/Products");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateProduct_Should_Return_Created()
        {
            await AuthenticateAsync();

            var request = new
            {
                ProductName = "Integration Test Product"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/Products", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        private async Task AuthenticateAsync()
        {
            var email = $"{Guid.NewGuid():N}@test.com";

            var registerRequest = new
            {
                UserName = $"user{Guid.NewGuid():N}",
                Email = email,
                Password = "Password@123"
            };

            await _client.PostAsJsonAsync("/api/v1/Auth/register", registerRequest);

            var loginRequest = new
            {
                Email = email,
                Password = "Password@123"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/Auth/login", loginRequest);

            response.EnsureSuccessStatusCode();

            var authResponse =
                await response.Content.ReadFromJsonAsync<AuthResponse>();

            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    authResponse!.AccessToken);
        }
    }
}
