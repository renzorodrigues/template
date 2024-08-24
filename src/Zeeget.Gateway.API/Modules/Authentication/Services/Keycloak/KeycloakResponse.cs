using System.Text.Json.Serialization;

namespace Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak
{
    public record KeycloakResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public long RefreshExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("not-before-policy")]
        public int NotBeforePolicy { get; set; }

        [JsonPropertyName("session_state")]
        public string? SessionState { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
    }
}
