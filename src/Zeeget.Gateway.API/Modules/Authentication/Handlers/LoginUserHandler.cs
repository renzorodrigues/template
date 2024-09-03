using MediatR;
using System.Net;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Api;
using Zeeget.Shared.Configurations.Settings.HttpClient;
using Zeeget.Shared.Guards;
using Zeeget.Shared.Services.HttpRequest.Interfaces;
using IResult = Zeeget.Shared.Api.IResult;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class LoginUserHandler(
        IHttpService httpService,
        IResult result,
        HttpClientSettings httpClientSettings
    ) : IRequestHandler<LoginUserQuery, Result>
    {
        private readonly IHttpService _httpService = httpService;
        private readonly IResult _result = result;
        private readonly HttpClientSettings _httpClientSettings = httpClientSettings;

        public async Task<Result> Handle(
            LoginUserQuery request,
            CancellationToken cancellationToken
        )
        {
            var settings = GetSettings();
            var formData = SetFormContent(request.Username, request.Password);

            var (Data, Response) = await _httpService
                .CreateRequest()
                .WithMethod(HttpMethod.Post)
                .WithRequestUri($"{settings.BaseAddress}{settings.RequestUri}")
                .WithFormUrlEncodedContent(formData)
                .SendWithHttpResponseAsync<KeycloakResponse>();

            if (Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return _result.Unauthorized();
            }

            return _result.Success(Data);
        }

        private static Dictionary<string, string> SetFormContent(string username, string password)
        {
            return new Dictionary<string, string>
            {
                { "client_id", "my-dotnet-app" },
                { "username", username },
                { "password", password },
                { "grant_type", "password" },
                { "client_secret", "ZQAo0noOBvtTXEx3OtTkuBV6ZNKOf38T" }
            };
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
