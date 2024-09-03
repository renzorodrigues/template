using MediatR;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Api;
using Zeeget.Shared.Guards;
using Zeeget.Shared.Services.HttpRequest.Interfaces;
using IResult = Zeeget.Shared.Api.IResult;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class RegisterUserHandler(
        IHttpService httpService,
        IResult result,
        HttpClientSettings httpClientSettings
    ) : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IHttpService _httpService = httpService;
        private readonly IResult _result = result;
        private readonly HttpClientSettings _httpClientSettings = httpClientSettings;

        public async Task<Result> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var adminTokenUrlSettings = GetAdminTokenUrlSettings();
            var formData = SetFormContent();

            var (Data, Response) = await _httpService
                .CreateRequest("GetToken")
                .WithMethod(HttpMethod.Post)
                .WithRequestUri(
                    $"{adminTokenUrlSettings.BaseAddress}{adminTokenUrlSettings.RequestUri}"
                )
                .WithFormUrlEncodedContent(formData)
                .SendWithHttpResponseAsync<KeycloakResponse>();

            var postUserSettings = GetPostUserSettings();

            if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _result.Unauthorized();
            }

            postUserSettings.Token = Data?.AccessToken;

            var result = await _httpService
                .CreateRequest("PostUser")
                .WithMethod(HttpMethod.Post)
                .WithHeader("Authorization", $"Bearer {postUserSettings.Token}")
                .WithStringContent(request)
                .WithRequestUri($"{postUserSettings.BaseAddress}{postUserSettings.RequestUri}")
                .SendAsync();

            if (result.IsSuccessStatusCode)
            {
                return _result.Created(result.Headers?.Location?.ToString().Split("/").Last());
            }

            return _result.BadRequest();
        }

        private static Dictionary<string, string> SetFormContent()
        {
            return new Dictionary<string, string>
            {
                { "client_id", "admin-cli" },
                { "username", "admin" },
                { "password", "renzors00" },
                { "grant_type", "password" }
            };
        }

        private AdminTokenUrlSettings GetAdminTokenUrlSettings()
        {
            var settings = ConfigurationGuard.EnsureConfiguration(
                _httpClientSettings.Keycloak,
                nameof(KeycloakSettings)
            );

            return ConfigurationGuard.EnsureConfiguration(
                settings.AdminTokenUrl,
                nameof(AdminTokenUrlSettings)
            );
        }

        private PostUserSettings GetPostUserSettings()
        {
            var settings = ConfigurationGuard.EnsureConfiguration(
                _httpClientSettings.Keycloak,
                nameof(KeycloakSettings)
            );

            return ConfigurationGuard.EnsureConfiguration(
                settings.PostUser,
                nameof(PostUserSettings)
            );
        }
    }
}
