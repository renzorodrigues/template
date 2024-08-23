using System.Text;
using Newtonsoft.Json;
using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Commons.Configurations.Settings.HttpClient;

namespace Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak
{
    public class KeycloakService : IKeycloakService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly HttpClientSettings _httpClientSettings;

        public KeycloakService(HttpClient httpClient, IConfiguration configuration, HttpClientSettings httpClientSettings)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClientSettings = httpClientSettings;
        }

        public async Task<bool> RegisterUserAsync(UserRegistrationDto user)
        {
            var registerUrl =
                $"{_configuration["Keycloak:BaseUrl"]}/auth/admin/realms/{_configuration["Keycloak:Realm"]}/users";

            var content = new StringContent(
                JsonConvert.SerializeObject(
                    new
                    {
                        username = user.Username,
                        email = user.Email,
                        enabled = true,
                        credentials = new[]
                        {
                            new
                            {
                                type = "password",
                                value = user.Password,
                                temporary = false
                            }
                        }
                    }
                ),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(registerUrl, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginUserAsync(UserLoginDto user)
        {
            var tokenUrl = $"{_httpClientSettings.KeycloakToken?.BaseAddress}{_httpClientSettings.KeycloakToken?.RequestUri}";

            var content = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>(
                        "client_id",
                        "my-dotnet-app"
                    ),
                    new KeyValuePair<string, string>("username", user.Username),
                    new KeyValuePair<string, string>("password", user.Password),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>(
                        "client_secret",
                        "ZQAo0noOBvtTXEx3OtTkuBV6ZNKOf38T"
                    )
                }
            );

            var response = await _httpClient.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result)[
                "access_token"
            ];

            return token;
        }
    }
}
