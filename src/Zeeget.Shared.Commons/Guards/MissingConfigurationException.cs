namespace Zeeget.Shared.Guards
{
    public class MissingConfigurationException(string configurationName)
        : InvalidOperationException(
            $"{configurationName} configuration is missing or not configured correctly."
        )
    { }
}
