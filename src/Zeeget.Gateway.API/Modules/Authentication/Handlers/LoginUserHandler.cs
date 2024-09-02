using MediatR;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Api;
using Zeeget.Shared.Configurations.Settings.HttpClient;
using Zeeget.Shared.Guards;
using Zeeget.Shared.Services.HttpClient.Interfaces;
using IResult = Zeeget.Shared.Api.IResult;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class LoginUserHandler(
        IHttpClient<UserLoginDto, KeycloakResponse> httpClient,
        IResult result,
        HttpClientSettings httpClientSettings
    ) : IRequestHandler<LoginUserQuery, Result>
    {
        private readonly IHttpClient<UserLoginDto, KeycloakResponse> _httpClient = httpClient;
        private readonly IResult _result = result;
        private readonly HttpClientSettings _httpClientSettings = httpClientSettings;

        public async Task<Result> Handle(
            LoginUserQuery request,
            CancellationToken cancellationToken
        )
        {
            var settings = GetSettings();

            var content = SetFormContent(request.User.Username, request.User.Password);

            var response = await _httpClient.SendAsync(request.User, settings, content);

            if (string.IsNullOrEmpty(response.AccessToken))
            {
                return _result.Unauthorized();
            }

            return _result.Success(response);
        }

        private static FormUrlEncodedContent SetFormContent(string username, string password)
        {
            return new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("client_id", "my-dotnet-app"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>(
                        "client_secret",
                        "ZQAo0noOBvtTXEx3OtTkuBV6ZNKOf38T"
                    )
                ]
            );
        }

        private TokenUrlSettings GetSettings()
        {
            var settings = ConfigurationGuard.EnsureConfiguration(
                _httpClientSettings.Keycloak,
                nameof(KeycloakSettings)
            );

            return ConfigurationGuard.EnsureConfiguration(
                settings.TokenUrl,
                nameof(TokenUrlSettings)
            );
        }
    }
}
