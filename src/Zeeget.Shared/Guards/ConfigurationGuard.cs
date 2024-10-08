﻿using Zeeget.Shared.Exceptions;

namespace Zeeget.Shared.Guards
{
    public static class ConfigurationGuard
    {
        public static T EnsureConfiguration<T>(T? value, string configurationName)
            where T : class
        {
            if (value is null)
            {
                throw new MissingConfigurationException(configurationName);
            }

            return value;
        }
    }
}
