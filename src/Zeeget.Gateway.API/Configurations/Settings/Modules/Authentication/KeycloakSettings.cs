﻿using Zeeget.Shared.Configurations.Settings.HttpClient;

namespace Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication
{
    public class KeycloakSettings
    {
        public TokenUrlSettings? TokenUrl { get; set; }
        public PostUserSettings? PostUser { get; set; }
        public AdminTokenUrlSettings? AdminTokenUrl { get; set; }
    }
}
