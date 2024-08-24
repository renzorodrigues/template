using MediatR;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Commons.Api.CustomResponse;
using Zeeget.Shared.Commons.Configurations.Settings.HttpClient;
using Zeeget.Shared.Commons.Services.HttpClient.Interfaces;
using Zeeget.Shared.Utilities.Guard;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class LoginUserHandler(
        IHttpClient<UserLoginDto, KeycloakResponse> httpClient,
        HttpClientSettings httpClientSettings
    ) : IRequestHandler<LoginUserQuery, Response<string>>
    {
        private readonly IHttpClient<UserLoginDto, KeycloakResponse> _httpClient = httpClient;
        private readonly HttpClientSettings _httpClientSettings = httpClientSettings;

        public async Task<Response<string>> Handle(
            LoginUserQuery request,
            CancellationToken cancellationToken
        )
        {
            var tokenUrl = ValidateSettings();

            var content = SetFormContent(request.User.Username, request.User.Password);

            var response = await _httpClient.SendAsync(request.User, tokenUrl, content);

            return Response<string>.RequestOk(response.AccessToken);
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

        private TokenUrlSettings ValidateSettings()
        {
            var settings = ConfigurationGuard.EnsureConfiguration(
                _httpClientSettings.Keycloak,
                nameof(KeycloakSettings)
            );

            return ConfigurationGuard.EnsureConfiguration(settings.TokenUrl, nameof(TokenUrlSettings));
        }
    }
}
