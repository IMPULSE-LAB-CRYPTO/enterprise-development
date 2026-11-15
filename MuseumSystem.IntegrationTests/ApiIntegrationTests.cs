using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aspire.Hosting.Testing;
using MuseumSystem.Contracts.Models;
using Xunit;

namespace MuseumSystem.IntegrationTests;

public class ApiIntegrationTests
{
    [Fact]
    public async Task Exhibitions_Endpoint_ReturnsSuccess_And_Seeded_Data()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.MuseumSystem_AppHost>();

        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        await app.ResourceNotifications.WaitForResourceHealthyAsync("museum-api");

        using var httpClient = app.CreateHttpClient("museum-api");

        // Сначала логинимся, чтобы получить токен
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "password"
        };
        var loginResponse = await httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginData);

        // Устанавливаем токен в заголовок Authorization
        httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", loginData.Token);

        // Делаем запрос к защищенному эндпоинту
        using var response = await httpClient.GetAsync("/api/exhibitions");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var exhibitions = await response.Content.ReadFromJsonAsync<List<ExhibitionResponse>>();
        Assert.NotNull(exhibitions);
        Assert.NotEmpty(exhibitions); 
    }

    [Fact]
    public async Task Auth_Login_With_Valid_Credentials_Returns_Token()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.MuseumSystem_AppHost>();

        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        await app.ResourceNotifications.WaitForResourceHealthyAsync("museum-api");

        using var httpClient = app.CreateHttpClient("museum-api");
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "password"
        };

        using var response = await httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
    }
}