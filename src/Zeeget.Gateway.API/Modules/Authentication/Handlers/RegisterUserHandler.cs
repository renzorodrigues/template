using MediatR;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Api;
using Zeeget.Shared.Guards;
using Zeeget.Shared.Services.HttpClient.Interfaces;
using IResult = Zeeget.Shared.Api.IResult;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IHttpClient<RegisterUserCommand, KeycloakResponse> _httpClientAdminToken;
        private readonly IHttpClient<RegisterUserCommand, bool> _httpClientPostUser;
        private readonly IResult _result;
        private readonly HttpClientSettings _httpClientSettings;

        public RegisterUserHandler(
            IHttpClient<RegisterUserCommand, KeycloakResponse> httpClientAdminToken,
            IHttpClient<RegisterUserCommand, bool> httpClientPostUser,
            IResult result,
            HttpClientSettings httpClientSettings
        )
        {
            _httpClientAdminToken = httpClientAdminToken;
            _httpClientPostUser = httpClientPostUser;
            _result = result;
            _httpClientSettings = httpClientSettings;
        }

        public async Task<Result> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var adminTokenUrlSettings = GetAdminTokenUrlSettings();

            var content = SetFormContent();

            var adminAccessToken = await _httpClientAdminToken.SendAsync(adminTokenUrlSettings, content);

            if (string.IsNullOrEmpty(adminAccessToken.AccessToken))
            {
                return _result.Error();
            }

            var postUserSettings = GetPostUserSettings();

            postUserSettings.Token = adminAccessToken.AccessToken;

            var result = await _httpClientPostUser.SendAsync(request, postUserSettings);

            return _result.Success(result);
        }

        private static FormUrlEncodedContent SetFormContent()
        {
            return new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("client_id", "admin-cli"),
                    new KeyValuePair<string, string>("username", "admin"),
                    new KeyValuePair<string, string>("password", "renzors00"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                ]
            );
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
